using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WebUI.Models;
using WebUI.Controllers;
using WebUI.Infrastructure;
using WebUI.Extension;
using MvcSiteMapProvider;
using MvcSiteMapProvider.Web.Mvc.Filters;
using Business.Abstract;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System.IO;
using System.Threading;
using Business.Entities;
using WebUI.Models.MProject;

namespace WebUI.Controllers
{
    public class MProjectController : MyController
    {
		private IMProjectRepository RepoMProject;
		private ICompanyRepository RepoCompany;
		private IContractorRepository RepoContractor;
		private IProjectRepository RepoProject;
		private ILogRepository RepoLog;

        public MProjectController(IMProjectRepository repoMProject, ILogRepository repoLog, ICompanyRepository repoCompany,IContractorRepository repoContractor,IProjectRepository repoProject)
			: base(repoLog)
        {
            RepoMProject = repoMProject;
			RepoCompany = repoCompany;
			RepoContractor = repoContractor;
			RepoProject = repoProject;
        }

		[MvcSiteMapNode(Title = "M Project", ParentKey = "Dashboard",Key="IndexMProject")]
		[SiteMapTitle("Breadcrumb")]
        public ActionResult Index()
        {
            return View();
        }

        public string Binding()
        {
            //kamus
            GridRequestParameters param = GridRequestParameters.Current;

            List<m_project> items = RepoMProject.FindAll(param.Skip, param.Take, (param.Sortings != null ? param.Sortings.ToList() : null), param.Filters);
            int total = RepoMProject.Count(param.Filters);

            return new JavaScriptSerializer().Serialize(new { total = total, data = new MProjectPresentationStub().MapList(items) });
        }

		[MvcSiteMapNode(Title = "Create", ParentKey = "IndexMProject")]
		[SiteMapTitle("Breadcrumb")]
        public ActionResult Create()
        {
			List<Business.Entities.company> listCompany = RepoCompany.FindAll();
			List<Business.Entities.contractor> listContractor = RepoContractor.FindAll();
			List<Business.Entities.project> listProject = RepoProject.FindAll();
			
            MProjectFormStub formStub = new MProjectFormStub(listCompany,listContractor,listProject);

            return View("Form", formStub);
        }

        [HttpPost]
		[SiteMapTitle("Breadcrumb")]
        public ActionResult Create(MProjectFormStub model)
        {
            //bool isNameExist = RepoMProject.Find().Where(p => p.name == model.Name).Count() > 0;
            
            if (ModelState.IsValid)
            {
                m_project dbItem = new m_project();
                dbItem = model.GetDbObject(dbItem);

                try
                {
                    RepoMProject.Save(dbItem);
                }
                catch (Exception e)
                {
				model.FillCompanyOptions(RepoCompany.FindAll());
				model.FillContractorOptions(RepoContractor.FindAll());
				model.FillProjectOptions(RepoProject.FindAll());
                    return View("Form", model);
                }

                //message
                string template = HttpContext.GetGlobalResourceObject("MyGlobalMessage", "CreateSuccess").ToString();
                this.SetMessage(model.Name, template);

                return RedirectToAction("Index");
            }
            else
            {
				model.FillCompanyOptions(RepoCompany.FindAll());
				model.FillContractorOptions(RepoContractor.FindAll());
				model.FillProjectOptions(RepoProject.FindAll());
                return View("Form", model);
            }
        }

		[MvcSiteMapNode(Title = "Edit", ParentKey = "IndexMProject", Key = "EditMProject", PreservedRouteParameters = "id")]
		[SiteMapTitle("Breadcrumb")]
        public ActionResult Edit(int id)
        {
            m_project m_project = RepoMProject.FindByPk(id);
			List<Business.Entities.company> listCompany = RepoCompany.FindAll();
			List<Business.Entities.contractor> listContractor = RepoContractor.FindAll();
			List<Business.Entities.project> listProject = RepoProject.FindAll();
            MProjectFormStub formStub = new MProjectFormStub(m_project,listCompany,listContractor,listProject);
            return View("Form", formStub);
        }

        [HttpPost]
		[SiteMapTitle("Breadcrumb")]
        public ActionResult Edit(MProjectFormStub model)
        {
            //bool isNameExist = RepoKompetitor.Find().Where(p => p.name == model.Name && p.id != model.Id).Count() > 0;

            if (ModelState.IsValid)
            {
                m_project dbItem = RepoMProject.FindByPk(model.Id);
                dbItem = model.GetDbObject(dbItem);

                try
                {
                    RepoMProject.Save(dbItem);
                }
                catch (Exception e)
                { 
				model.FillCompanyOptions(RepoCompany.FindAll());
				model.FillContractorOptions(RepoContractor.FindAll());
				model.FillProjectOptions(RepoProject.FindAll());
                    return View("Form", model);
                }

                //message
                string template = HttpContext.GetGlobalResourceObject("MyGlobalMessage", "CreateSuccess").ToString();
                this.SetMessage(model.Name, template);

                return RedirectToAction("Index");
            }
            else
            {
				model.FillCompanyOptions(RepoCompany.FindAll());
				model.FillContractorOptions(RepoContractor.FindAll());
				model.FillProjectOptions(RepoProject.FindAll());
                return View("Form", model);
            }
        }

		[HttpPost]
		public JsonResult Delete(int id)
        {
			string template = "";
			ResponseModel response = new ResponseModel(true);
			m_project dbItem = RepoMProject.FindByPk(id);

            RepoMProject.Delete(dbItem);

            return Json(response);
        }

		#region ImportExcel

		public ViewResult ImportExcel()
        {
            MProjectImportExcelStub model = new MProjectImportExcelStub();

            return View("ImportExcelForm",model);
        }


		[HttpPost]
        public ActionResult ImportExcel(MProjectImportExcelStub model)
        {
            if (ModelState.IsValid)
            {

                XSSFSheet sheet; int colCount; int rowCount; int startRow = 3; int colNum; int id;
                List<string> messageList = new List<string>();

                string currentFilePathInServer = Server.MapPath(model.ExcelFilePath);
                XSSFWorkbook book;
                try
                {
                    using (FileStream file = new FileStream(currentFilePathInServer, FileMode.Open, FileAccess.Read))
                    {
                        book = new XSSFWorkbook(file);
                    }

                    sheet = (XSSFSheet)book.GetSheet("Data");
                    rowCount = sheet.LastRowNum;
                    colNum = 0;
                    id = 0;
                    bool allowSave = true;
                    for (int row = startRow; row <= rowCount; row++)
                    {
                        colNum = 0;
                        var dt = (XSSFCell)sheet.GetRow(row).GetCell(colNum);
                        if (dt != null)
                        {
                            allowSave = true;
                            sheet.GetRow(row).GetCell(colNum).SetCellType(CellType.String);
                            if (sheet.GetRow(row).GetCell(colNum).StringCellValue != "")
                            {
                                    m_project insertData = new m_project();

                                    m_project filledData = this.FillDataFromExcel(insertData, sheet, row, colNum,ref messageList, ref allowSave);

                                    if (allowSave == true)
                                    {
                                        RepoMProject.Save(filledData);
                                    }
                            }
                        }
                    }

                }
                catch (Exception e)
                {
                    messageList.Add("An error has occurred. Please contact administrator.");
                }
                ViewBag.MessageList = messageList;
                return View("ImportExcelForm", model);
                //return ret;
            }
            else
            {
                ViewBag.MessageList = null;
                return View("ImportExcelForm", model);
            }
        }


		public ActionResult GetExcelTemplate()
        {
List<Business.Entities.company> listCompany = RepoCompany.FindAll();
List<Business.Entities.contractor> listContractor = RepoContractor.FindAll();
List<Business.Entities.project> listProject = RepoProject.FindAll();


            string filename = "Template M Project.xlsx";
            byte[] excel =MProjectImportExcelStub.GenerateTemplate(listCompany,listContractor,listProject); //"~/App_Data/template/licensor.xls"
            return File(excel, "application/x-msexcel", filename);
        }


		private m_project FillDataFromExcel(m_project insertData,XSSFSheet sheet, int row, int colNum,ref List<string> messageList, ref bool allowSave)
        {
            bool test;
            var dt = (XSSFCell)sheet.GetRow(row).GetCell(colNum);

			    				//required non-foreign key Name (string)
												colNum=0;
								dt = (XSSFCell)sheet.GetRow(row).GetCell(colNum);
								if (dt != null)
								{
									sheet.GetRow(row).GetCell(colNum).SetCellType(CellType.String);
									if (sheet.GetRow(row).GetCell(colNum).StringCellValue != String.Empty)
									{
										insertData.name = sheet.GetRow(row).GetCell(colNum).StringCellValue;
									}
									else
									{
										allowSave = false;
										messageList.Add(GetExcelMessage(row, "Name", "Must be filled"));
									}
                
								}
							           				//optional foreign key Contractor Id
				colNum = 1;
            dt = (XSSFCell)sheet.GetRow(row).GetCell(colNum);
            if (dt != null)
            {
                sheet.GetRow(row).GetCell(colNum).SetCellType(CellType.String);
                if (sheet.GetRow(row).GetCell(colNum).StringCellValue != String.Empty)
                {
                    //insertData.contractor_id = RepoContractor.GetIdByNameAndInsert(sheet.GetRow(row).GetCell(colNum).StringCellValue);
                    //if(insertData.contractor_id == 0)
                    //{
                    //    allowSave = false;
                    //    messageList.Add(GetExcelMessage(row, "Contractor", "Does not exist"));
                    //}
				}
            }
            				//required non-foreign key Photo (string)
												colNum=2;
								dt = (XSSFCell)sheet.GetRow(row).GetCell(colNum);
								if (dt != null)
								{
									sheet.GetRow(row).GetCell(colNum).SetCellType(CellType.String);
									if (sheet.GetRow(row).GetCell(colNum).StringCellValue != String.Empty)
									{
										insertData.photo = sheet.GetRow(row).GetCell(colNum).StringCellValue;
									}
									else
									{
										allowSave = false;
										messageList.Add(GetExcelMessage(row, "Photo", "Must be filled"));
									}
                
								}
							           				//required non-foreign key Description (string)
												colNum=3;
								dt = (XSSFCell)sheet.GetRow(row).GetCell(colNum);
								if (dt != null)
								{
									sheet.GetRow(row).GetCell(colNum).SetCellType(CellType.String);
									if (sheet.GetRow(row).GetCell(colNum).StringCellValue != String.Empty)
									{
										insertData.description = sheet.GetRow(row).GetCell(colNum).StringCellValue;
									}
									else
									{
										allowSave = false;
										messageList.Add(GetExcelMessage(row, "Description", "Must be filled"));
									}
                
								}
							           				//required non-foreign key Start Date (System.DateTime)
													colNum=4;
									dt = (XSSFCell)sheet.GetRow(row).GetCell(colNum);
									if (dt != null)
									{
										sheet.GetRow(row).GetCell(colNum).SetCellType(CellType.String);
										if (sheet.GetRow(row).GetCell(colNum).StringCellValue != String.Empty)
										{
											DateTime dateVal;
											double dateOA;

											test = Double.TryParse(sheet.GetRow(row).GetCell(colNum).StringCellValue, out dateOA);
											if (test == false)
											{
												allowSave = false;
												messageList.Add(GetExcelMessage(row, "Start Date", "Invalid value"));
											}
											else
											{
												test = DateTime.TryParse(DateTime.FromOADate(dateOA).ToString(), out dateVal);
												insertData.start_date = dateVal;
											}
                    
										}
										else
										{
											allowSave = false;
											messageList.Add(GetExcelMessage(row, "Start Date", "Must be filled"));
										}
                
									}

									           				//required non-foreign key Finish Date (System.DateTime)
													colNum=5;
									dt = (XSSFCell)sheet.GetRow(row).GetCell(colNum);
									if (dt != null)
									{
										sheet.GetRow(row).GetCell(colNum).SetCellType(CellType.String);
										if (sheet.GetRow(row).GetCell(colNum).StringCellValue != String.Empty)
										{
											DateTime dateVal;
											double dateOA;

											test = Double.TryParse(sheet.GetRow(row).GetCell(colNum).StringCellValue, out dateOA);
											if (test == false)
											{
												allowSave = false;
												messageList.Add(GetExcelMessage(row, "Finish Date", "Invalid value"));
											}
											else
											{
												test = DateTime.TryParse(DateTime.FromOADate(dateOA).ToString(), out dateVal);
												insertData.finish_date = dateVal;
											}
                    
										}
										else
										{
											allowSave = false;
											messageList.Add(GetExcelMessage(row, "Finish Date", "Must be filled"));
										}
                
									}

									           				//optional non-foreign key Highlight (bool?)
											colNum=6;
							dt = (XSSFCell)sheet.GetRow(row).GetCell(colNum);
							if (dt != null)
							{
								sheet.GetRow(row).GetCell(colNum).SetCellType(CellType.String);
								if (sheet.GetRow(row).GetCell(colNum).StringCellValue != String.Empty)
								{
									string boolValue = sheet.GetRow(row).GetCell(colNum).StringCellValue;
									if(boolValue.ToLower() != "yes" && boolValue.ToLower() != "no")
									{
										allowSave = false;
										messageList.Add(GetExcelMessage(row, "Highlight", "Must be filled with Yes or No"));
									}
									else
									{
										bool val = false;
										if(boolValue.ToLower() == "yes")
										{
											val = true;
										}
										insertData.highlight = val;
									} 
									
								}
                
							}
						           				//required non-foreign key Project Stage (string)
												colNum=7;
								dt = (XSSFCell)sheet.GetRow(row).GetCell(colNum);
								if (dt != null)
								{
									sheet.GetRow(row).GetCell(colNum).SetCellType(CellType.String);
									if (sheet.GetRow(row).GetCell(colNum).StringCellValue != String.Empty)
									{
										insertData.project_stage = sheet.GetRow(row).GetCell(colNum).StringCellValue;
									}
									else
									{
										allowSave = false;
										messageList.Add(GetExcelMessage(row, "Project Stage", "Must be filled"));
									}
                
								}
							           				//optional non-foreign key Status (byte?)
													colNum=8;
									dt = (XSSFCell)sheet.GetRow(row).GetCell(colNum);
									if (dt != null)
									{
										sheet.GetRow(row).GetCell(colNum).SetCellType(CellType.String);
										if (sheet.GetRow(row).GetCell(colNum).StringCellValue != String.Empty)
										{
											int val = 0;
											test = Int32.TryParse(sheet.GetRow(row).GetCell(colNum).StringCellValue, out val);
											if (test == false)
											{
												allowSave = false;
												messageList.Add(GetExcelMessage(row, "Status", "Invalid value"));
											}
											else
											{
												insertData.status= (byte)val;
											}
                    
										}
                
									}
									           				//optional non-foreign key Budget (double?)
													colNum=9;
									dt = (XSSFCell)sheet.GetRow(row).GetCell(colNum);
									if (dt != null)
									{
										sheet.GetRow(row).GetCell(colNum).SetCellType(CellType.String);
										if (sheet.GetRow(row).GetCell(colNum).StringCellValue != String.Empty)
										{
											double val = 0;
											test = Double.TryParse(sheet.GetRow(row).GetCell(colNum).StringCellValue, out val);
											if (test == false)
											{
												allowSave = false;
												messageList.Add(GetExcelMessage(row, "Budget", "Invalid value"));
											}
											else
											{
												insertData.budget= val;
											}
                    
										}
                
									}
									           				//required non-foreign key Currency (string)
												colNum=10;
								dt = (XSSFCell)sheet.GetRow(row).GetCell(colNum);
								if (dt != null)
								{
									sheet.GetRow(row).GetCell(colNum).SetCellType(CellType.String);
									if (sheet.GetRow(row).GetCell(colNum).StringCellValue != String.Empty)
									{
										insertData.currency = sheet.GetRow(row).GetCell(colNum).StringCellValue;
									}
									else
									{
										allowSave = false;
										messageList.Add(GetExcelMessage(row, "Currency", "Must be filled"));
									}
                
								}
							           				//optional non-foreign key Num (int?)
													colNum=11;
									dt = (XSSFCell)sheet.GetRow(row).GetCell(colNum);
									if (dt != null)
									{
										sheet.GetRow(row).GetCell(colNum).SetCellType(CellType.String);
										if (sheet.GetRow(row).GetCell(colNum).StringCellValue != String.Empty)
										{
											int val = 0;
											test = Int32.TryParse(sheet.GetRow(row).GetCell(colNum).StringCellValue, out val);
											if (test == false)
											{
												allowSave = false;
												messageList.Add(GetExcelMessage(row, "Num", "Invalid value"));
											}
											else
											{
												insertData.num= val;
											}
                    
										}
                
									}
									           				//optional foreign key Pmc Id
				colNum = 12;
            dt = (XSSFCell)sheet.GetRow(row).GetCell(colNum);
            if (dt != null)
            {
                sheet.GetRow(row).GetCell(colNum).SetCellType(CellType.String);
                if (sheet.GetRow(row).GetCell(colNum).StringCellValue != String.Empty)
                {
                    //insertData.pmc_id = RepoPmc.GetIdByNameAndInsert(sheet.GetRow(row).GetCell(colNum).StringCellValue);
                    //if(insertData.pmc_id == 0)
                    //{
                    //    allowSave = false;
                    //    messageList.Add(GetExcelMessage(row, "Pmc", "Does not exist"));
                    //}
				}
            }
            				//required non-foreign key Summary (string)
												colNum=13;
								dt = (XSSFCell)sheet.GetRow(row).GetCell(colNum);
								if (dt != null)
								{
									sheet.GetRow(row).GetCell(colNum).SetCellType(CellType.String);
									if (sheet.GetRow(row).GetCell(colNum).StringCellValue != String.Empty)
									{
										insertData.summary = sheet.GetRow(row).GetCell(colNum).StringCellValue;
									}
									else
									{
										allowSave = false;
										messageList.Add(GetExcelMessage(row, "Summary", "Must be filled"));
									}
                
								}
							           				//optional foreign key Company Id
				colNum = 14;
            dt = (XSSFCell)sheet.GetRow(row).GetCell(colNum);
            if (dt != null)
            {
                sheet.GetRow(row).GetCell(colNum).SetCellType(CellType.String);
                if (sheet.GetRow(row).GetCell(colNum).StringCellValue != String.Empty)
                {
                    //insertData.company_id = RepoCompany.GetIdByNameAndInsert(sheet.GetRow(row).GetCell(colNum).StringCellValue);
                    //if(insertData.company_id == 0)
                    //{
                    //    allowSave = false;
                    //    messageList.Add(GetExcelMessage(row, "Company", "Does not exist"));
                    //}
				}
            }
            				//required non-foreign key Status Non Technical (string)
												colNum=15;
								dt = (XSSFCell)sheet.GetRow(row).GetCell(colNum);
								if (dt != null)
								{
									sheet.GetRow(row).GetCell(colNum).SetCellType(CellType.String);
									if (sheet.GetRow(row).GetCell(colNum).StringCellValue != String.Empty)
									{
										insertData.status_non_technical = sheet.GetRow(row).GetCell(colNum).StringCellValue;
									}
									else
									{
										allowSave = false;
										messageList.Add(GetExcelMessage(row, "Status Non Technical", "Must be filled"));
									}
                
								}
							           				//required non-foreign key Is Completed (bool)
											colNum=16;
							dt = (XSSFCell)sheet.GetRow(row).GetCell(colNum);
							if (dt != null)
							{
								sheet.GetRow(row).GetCell(colNum).SetCellType(CellType.String);
								if (sheet.GetRow(row).GetCell(colNum).StringCellValue != String.Empty)
								{
									string boolValue = sheet.GetRow(row).GetCell(colNum).StringCellValue;
									if(boolValue.ToLower() != "yes" && boolValue.ToLower() != "no")
									{
										allowSave = false;
										messageList.Add(GetExcelMessage(row, "Is Completed", "Must be filled with Yes or No"));
									}
									else
									{
										bool val = false;
										if(boolValue.ToLower() == "yes")
										{
											val = true;
										}
										insertData.is_completed = val;
									} 
									
								}
								else
								{
									allowSave = false;
									messageList.Add(GetExcelMessage(row, "Is Completed", "Must be filled"));
								}
                
							}
						           				//optional non-foreign key Completed Date (System.DateTime?)
													colNum=17;
									dt = (XSSFCell)sheet.GetRow(row).GetCell(colNum);
									if (dt != null)
									{
										sheet.GetRow(row).GetCell(colNum).SetCellType(CellType.String);
										if (sheet.GetRow(row).GetCell(colNum).StringCellValue != String.Empty)
										{
											DateTime dateVal;
											double dateOA;

											test = Double.TryParse(sheet.GetRow(row).GetCell(colNum).StringCellValue, out dateOA);
											if (test == false)
											{
												allowSave = false;
												messageList.Add(GetExcelMessage(row, "Completed Date", "Invalid value"));
											}
											else
											{
												test = DateTime.TryParse(DateTime.FromOADate(dateOA).ToString(), out dateVal);
												insertData.completed_date = dateVal;
											}
                    
										}
                
									}

									           				//required foreign key Project Id
				colNum = 18;
            dt = (XSSFCell)sheet.GetRow(row).GetCell(colNum);
            if (dt != null)
            {
                sheet.GetRow(row).GetCell(colNum).SetCellType(CellType.String);
                if (sheet.GetRow(row).GetCell(colNum).StringCellValue != String.Empty)
                {
                    insertData.project_id = RepoProject.GetIdByNameAndInsert(sheet.GetRow(row).GetCell(colNum).StringCellValue);
					if(insertData.project_id == 0)
					{
						allowSave = false;
						messageList.Add(GetExcelMessage(row, "Project", "Does not exist"));
					}
				}
                else
                {
                    allowSave = false;
                    messageList.Add(GetExcelMessage(row, "Project", "Must be filled"));
                }
            }
            				//required non-foreign key Submit For Approval Time (System.DateTime)
													colNum=19;
									dt = (XSSFCell)sheet.GetRow(row).GetCell(colNum);
									if (dt != null)
									{
										sheet.GetRow(row).GetCell(colNum).SetCellType(CellType.String);
										if (sheet.GetRow(row).GetCell(colNum).StringCellValue != String.Empty)
										{
											DateTime dateVal;
											double dateOA;

											test = Double.TryParse(sheet.GetRow(row).GetCell(colNum).StringCellValue, out dateOA);
											if (test == false)
											{
												allowSave = false;
												messageList.Add(GetExcelMessage(row, "Submit For Approval Time", "Invalid value"));
											}
											else
											{
												test = DateTime.TryParse(DateTime.FromOADate(dateOA).ToString(), out dateVal);
												insertData.submit_for_approval_time = dateVal;
											}
                    
										}
										else
										{
											allowSave = false;
											messageList.Add(GetExcelMessage(row, "Submit For Approval Time", "Must be filled"));
										}
                
									}

									           				//required non-foreign key Approval Status (string)
												colNum=20;
								dt = (XSSFCell)sheet.GetRow(row).GetCell(colNum);
								if (dt != null)
								{
									sheet.GetRow(row).GetCell(colNum).SetCellType(CellType.String);
									if (sheet.GetRow(row).GetCell(colNum).StringCellValue != String.Empty)
									{
										insertData.approval_status = sheet.GetRow(row).GetCell(colNum).StringCellValue;
									}
									else
									{
										allowSave = false;
										messageList.Add(GetExcelMessage(row, "Approval Status", "Must be filled"));
									}
                
								}
							           				//optional non-foreign key Approval Time (System.DateTime?)
													colNum=21;
									dt = (XSSFCell)sheet.GetRow(row).GetCell(colNum);
									if (dt != null)
									{
										sheet.GetRow(row).GetCell(colNum).SetCellType(CellType.String);
										if (sheet.GetRow(row).GetCell(colNum).StringCellValue != String.Empty)
										{
											DateTime dateVal;
											double dateOA;

											test = Double.TryParse(sheet.GetRow(row).GetCell(colNum).StringCellValue, out dateOA);
											if (test == false)
											{
												allowSave = false;
												messageList.Add(GetExcelMessage(row, "Approval Time", "Invalid value"));
											}
											else
											{
												test = DateTime.TryParse(DateTime.FromOADate(dateOA).ToString(), out dateVal);
												insertData.approval_time = dateVal;
											}
                    
										}
                
									}

									           				//required non-foreign key Deleted (bool)
											colNum=22;
							dt = (XSSFCell)sheet.GetRow(row).GetCell(colNum);
							if (dt != null)
							{
								sheet.GetRow(row).GetCell(colNum).SetCellType(CellType.String);
								if (sheet.GetRow(row).GetCell(colNum).StringCellValue != String.Empty)
								{
									string boolValue = sheet.GetRow(row).GetCell(colNum).StringCellValue;
									if(boolValue.ToLower() != "yes" && boolValue.ToLower() != "no")
									{
										allowSave = false;
										messageList.Add(GetExcelMessage(row, "Deleted", "Must be filled with Yes or No"));
									}
									else
									{
										bool val = false;
										if(boolValue.ToLower() == "yes")
										{
											val = true;
										}
										insertData.deleted = val;
									} 
									
								}
								else
								{
									allowSave = false;
									messageList.Add(GetExcelMessage(row, "Deleted", "Must be filled"));
								}
                
							}
						           				//required non-foreign key Approval Message (string)
												colNum=23;
								dt = (XSSFCell)sheet.GetRow(row).GetCell(colNum);
								if (dt != null)
								{
									sheet.GetRow(row).GetCell(colNum).SetCellType(CellType.String);
									if (sheet.GetRow(row).GetCell(colNum).StringCellValue != String.Empty)
									{
										insertData.approval_message = sheet.GetRow(row).GetCell(colNum).StringCellValue;
									}
									else
									{
										allowSave = false;
										messageList.Add(GetExcelMessage(row, "Approval Message", "Must be filled"));
									}
                
								}
							           				//required non-foreign key Status Technical (string)
												colNum=24;
								dt = (XSSFCell)sheet.GetRow(row).GetCell(colNum);
								if (dt != null)
								{
									sheet.GetRow(row).GetCell(colNum).SetCellType(CellType.String);
									if (sheet.GetRow(row).GetCell(colNum).StringCellValue != String.Empty)
									{
										insertData.status_technical = sheet.GetRow(row).GetCell(colNum).StringCellValue;
									}
									else
									{
										allowSave = false;
										messageList.Add(GetExcelMessage(row, "Status Technical", "Must be filled"));
									}
                
								}
							           				//required non-foreign key Scurve Data (string)
												colNum=25;
								dt = (XSSFCell)sheet.GetRow(row).GetCell(colNum);
								if (dt != null)
								{
									sheet.GetRow(row).GetCell(colNum).SetCellType(CellType.String);
									if (sheet.GetRow(row).GetCell(colNum).StringCellValue != String.Empty)
									{
										insertData.scurve_data = sheet.GetRow(row).GetCell(colNum).StringCellValue;
									}
									else
									{
										allowSave = false;
										messageList.Add(GetExcelMessage(row, "Scurve Data", "Must be filled"));
									}
                
								}
							           
            return insertData;
        }

		 private static string GetExcelMessage(int rowNumber, string columnName, string message)
        {
            string msg = String.Format("Row {0}. Column {1}. {2}", rowNumber + 1, columnName, message);

            return msg;
        }

		#endregion
	}
}

