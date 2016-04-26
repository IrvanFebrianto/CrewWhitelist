using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.Util;
using System.IO;
using System.Threading;
using Common.Enums;
using Business.Entities;

namespace WebUI.Models.MProject
{
    public class MProjectImportExcelStub
    {
		[DisplayName("File Excel")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.MyGlobalErrors))]
        public string ExcelFilePath { get; set; }

		public MProjectImportExcelStub() { }
		public static byte[] GenerateTemplate(List<Business.Entities.company> listCompany,List<Business.Entities.contractor> listContractor,List<Business.Entities.project> listProject)
        {
            //culture
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US"); //supaya file tidak corrupt
            int parseRecordNumber = 100; // number of rows that has style or validation
            int startRowIndex = 3;

            XSSFCellStyle styleCurrency;
            XSSFCellStyle styleDate;
            XSSFCellStyle styleNumeric;
			XSSFCellStyle styleDecimal;

            //kamus
            XSSFWorkbook workbook = new XSSFWorkbook();
            XSSFSheet sheet; XSSFRow row; XSSFCell cell;

            XSSFCellStyle style; XSSFFont font;

            CellRangeAddressList addressList; XSSFDataValidationHelper dvHelper; XSSFDataValidationConstraint dvConstraint; XSSFDataValidation validation;


			List<string> listCompanyString = new List<string>();
  foreach(var data in  listCompany)
  {
		listCompanyString.Add(data.name);
  }


List<string> listContractorString = new List<string>();
  foreach(var data in  listContractor)
  {
		listContractorString.Add(data.name);
  }


List<string> listProjectString = new List<string>();
  foreach(var data in  listProject)
  {
		listProjectString.Add(data.name);
  }



            styleCurrency = (XSSFCellStyle)workbook.CreateCellStyle();
            styleCurrency.DataFormat = workbook.CreateDataFormat().GetFormat("$#,##0.00_);($#,##0.00)");

            styleNumeric = (XSSFCellStyle)workbook.CreateCellStyle();
            styleNumeric.DataFormat = workbook.CreateDataFormat().GetFormat("#,##0");

            styleDate = (XSSFCellStyle)workbook.CreateCellStyle();
            styleDate.DataFormat = workbook.CreateDataFormat().GetFormat("mm/dd/yyyy");

			         styleDecimal = (XSSFCellStyle)workbook.CreateCellStyle();
            styleDecimal.DataFormat = workbook.CreateDataFormat().GetFormat("0.00");

            List<string> columnList = new List<string>();
			    			columnList.Add("Name");
           			int ContractorStringLocation = 1; 
			columnList.Add("Contractor");
            			columnList.Add("Photo");
           			columnList.Add("Description");
           			columnList.Add("Start Date");
           			columnList.Add("Finish Date");
           			columnList.Add("Highlight");
           			columnList.Add("Project Stage");
           			columnList.Add("Status");
           			columnList.Add("Budget");
           			columnList.Add("Currency");
           			columnList.Add("Num");
           			int PmcStringLocation = 12; 
			columnList.Add("Pmc");
            			columnList.Add("Summary");
           			int CompanyStringLocation = 14; 
			columnList.Add("Company");
            			columnList.Add("Status Non Technical");
           			columnList.Add("Is Completed");
           			columnList.Add("Completed Date");
           			int ProjectStringLocation = 18; 
			columnList.Add("Project");
            			columnList.Add("Submit For Approval Time");
           			columnList.Add("Approval Status");
           			columnList.Add("Approval Time");
           			columnList.Add("Deleted");
           			columnList.Add("Approval Message");
           			columnList.Add("Status Technical");
           			columnList.Add("Scurve Data");
           
            sheet = (XSSFSheet)workbook.CreateSheet("Data");
            int col = 0;
            int rowNumber = 0;
            //create row (header)
            row = (XSSFRow)sheet.CreateRow((short)rowNumber);
            dvHelper = new XSSFDataValidationHelper(sheet);
            //header data
            style = (XSSFCellStyle)workbook.CreateCellStyle();
            cell = (XSSFCell)row.CreateCell(col);
            cell.SetCellValue("M Project");
            font = (XSSFFont)workbook.CreateFont();
            font.FontHeight = 24;
            style.SetFont(font);
            cell.CellStyle = style;
            rowNumber++;
            row = (XSSFRow)sheet.CreateRow((short)rowNumber);

            style = (XSSFCellStyle)workbook.CreateCellStyle();
            font = (XSSFFont)workbook.CreateFont();
            font.Boldweight = (short)FontBoldWeight.Bold;
            style.SetFont(font);
            rowNumber++;
            row = (XSSFRow)sheet.CreateRow((short)rowNumber);
            //header data
            foreach (string data in columnList)
            {
                cell = (XSSFCell)row.CreateCell(col);
                cell.SetCellValue(data);

                cell.CellStyle = style;
                //cell.CellStyle.IsLocked = true;

                //column width
                sheet.SetColumnWidth(col, (30 * 256));
                ++col;
            }

            //sheet.CreateFreezePane(0, 4);
					
		//dropdownlist Company
		if(listCompanyString.Count > 0)
		{
				
				XSSFSheet hidden = (XSSFSheet)workbook.CreateSheet("MasterCompany");
				int i=0;
				foreach(string a in listCompanyString)
				{
					row = (XSSFRow)hidden.CreateRow(i);
                    cell = (XSSFCell)row.CreateCell(0);
					cell.SetCellValue(a);
					i++;
				}

				validation = null;
				dvConstraint = null;
				dvHelper = null;
				dvHelper=new XSSFDataValidationHelper(sheet);
				addressList = new  CellRangeAddressList(startRowIndex,parseRecordNumber,CompanyStringLocation,CompanyStringLocation);
				 dvConstraint = (XSSFDataValidationConstraint)dvHelper.CreateFormulaListConstraint("MasterCompany!$A$1:$A$" + listCompanyString.Count);
				validation = (XSSFDataValidation)dvHelper.CreateValidation(dvConstraint, addressList);
                validation.SuppressDropDownArrow = true;
                validation.ShowErrorBox = true;
				workbook.SetSheetHidden(1, true);
                sheet.AddValidationData(validation);
		}
		
				
		//dropdownlist Contractor
		if(listContractorString.Count > 0)
		{
				
				XSSFSheet hidden = (XSSFSheet)workbook.CreateSheet("MasterContractor");
				int i=0;
				foreach(string a in listContractorString)
				{
					row = (XSSFRow)hidden.CreateRow(i);
                    cell = (XSSFCell)row.CreateCell(0);
					cell.SetCellValue(a);
					i++;
				}

				validation = null;
				dvConstraint = null;
				dvHelper = null;
				dvHelper=new XSSFDataValidationHelper(sheet);
				addressList = new  CellRangeAddressList(startRowIndex,parseRecordNumber,ContractorStringLocation,ContractorStringLocation);
				 dvConstraint = (XSSFDataValidationConstraint)dvHelper.CreateFormulaListConstraint("MasterContractor!$A$1:$A$" + listContractorString.Count);
				validation = (XSSFDataValidation)dvHelper.CreateValidation(dvConstraint, addressList);
                validation.SuppressDropDownArrow = true;
                validation.ShowErrorBox = true;
				workbook.SetSheetHidden(2, true);
                sheet.AddValidationData(validation);
		}
		
				
		//dropdownlist Project
		if(listProjectString.Count > 0)
		{
				
				XSSFSheet hidden = (XSSFSheet)workbook.CreateSheet("MasterProject");
				int i=0;
				foreach(string a in listProjectString)
				{
					row = (XSSFRow)hidden.CreateRow(i);
                    cell = (XSSFCell)row.CreateCell(0);
					cell.SetCellValue(a);
					i++;
				}

				validation = null;
				dvConstraint = null;
				dvHelper = null;
				dvHelper=new XSSFDataValidationHelper(sheet);
				addressList = new  CellRangeAddressList(startRowIndex,parseRecordNumber,ProjectStringLocation,ProjectStringLocation);
				 dvConstraint = (XSSFDataValidationConstraint)dvHelper.CreateFormulaListConstraint("MasterProject!$A$1:$A$" + listProjectString.Count);
				validation = (XSSFDataValidation)dvHelper.CreateValidation(dvConstraint, addressList);
                validation.SuppressDropDownArrow = true;
                validation.ShowErrorBox = true;
				workbook.SetSheetHidden(3, true);
                sheet.AddValidationData(validation);
		}
		
		
            /*Cell formatting*/
            for (int i = startRowIndex; i <= parseRecordNumber; i++)
            {
                rowNumber++;
                row = (XSSFRow)sheet.CreateRow((short)rowNumber);

							        			
		            			//start_date
			col = 4;
            cell = (XSSFCell)row.CreateCell((short)col);
			cell.CellStyle = styleDate;
		    
			if(i==startRowIndex)
			{
				
						
						addressList = new CellRangeAddressList(startRowIndex, parseRecordNumber, 4, 4);
						dvConstraint = (XSSFDataValidationConstraint)dvHelper.CreateDateConstraint(OperatorType.GREATER_THAN, "01/01/1900", "", "mm/dd/yyyy");
						validation = (XSSFDataValidation)dvHelper.CreateValidation(dvConstraint, addressList);
						validation.ShowErrorBox = true;
						validation.CreateErrorBox("Input Error", "Value must be a date, example 12/30/2000");
						sheet.AddValidationData(validation);

                  			}
           			//finish_date
			col = 5;
            cell = (XSSFCell)row.CreateCell((short)col);
			cell.CellStyle = styleDate;
		    
			if(i==startRowIndex)
			{
				
						
						addressList = new CellRangeAddressList(startRowIndex, parseRecordNumber, 5, 5);
						dvConstraint = (XSSFDataValidationConstraint)dvHelper.CreateDateConstraint(OperatorType.GREATER_THAN, "01/01/1900", "", "mm/dd/yyyy");
						validation = (XSSFDataValidation)dvHelper.CreateValidation(dvConstraint, addressList);
						validation.ShowErrorBox = true;
						validation.CreateErrorBox("Input Error", "Value must be a date, example 12/30/2000");
						sheet.AddValidationData(validation);

                  			}
                       			//budget
			col = 9;
            cell = (XSSFCell)row.CreateCell((short)col);
			cell.CellStyle = styleDecimal;
		    
			if(i==startRowIndex)
			{
							}
               			//num
			col = 11;
            cell = (XSSFCell)row.CreateCell((short)col);
			cell.CellStyle = styleNumeric;
		    
			if(i==startRowIndex)
			{
										addressList = new CellRangeAddressList(startRowIndex, parseRecordNumber, 11, 11);
						dvHelper = new XSSFDataValidationHelper(sheet);
						dvConstraint = (XSSFDataValidationConstraint)dvHelper.CreateNumericConstraint(ValidationType.INTEGER, OperatorType.BETWEEN, "0", "1000000000000000000");
						validation = (XSSFDataValidation)dvHelper.CreateValidation(dvConstraint, addressList);
						validation.ShowErrorBox = true;
						validation.CreateErrorBox("Input Error", "Value must be a number, maximum 1.000.000.000.000.000.000");
						sheet.AddValidationData(validation);
							}
           			
		        			
		            			//completed_date
			col = 17;
            cell = (XSSFCell)row.CreateCell((short)col);
			cell.CellStyle = styleDate;
		    
			if(i==startRowIndex)
			{
				
						
						addressList = new CellRangeAddressList(startRowIndex, parseRecordNumber, 17, 17);
						dvConstraint = (XSSFDataValidationConstraint)dvHelper.CreateDateConstraint(OperatorType.GREATER_THAN, "01/01/1900", "", "mm/dd/yyyy");
						validation = (XSSFDataValidation)dvHelper.CreateValidation(dvConstraint, addressList);
						validation.ShowErrorBox = true;
						validation.CreateErrorBox("Input Error", "Value must be a date, example 12/30/2000");
						sheet.AddValidationData(validation);

                  			}
           			
		    			//submit_for_approval_time
			col = 19;
            cell = (XSSFCell)row.CreateCell((short)col);
			cell.CellStyle = styleDate;
		    
			if(i==startRowIndex)
			{
				
						
						addressList = new CellRangeAddressList(startRowIndex, parseRecordNumber, 19, 19);
						dvConstraint = (XSSFDataValidationConstraint)dvHelper.CreateDateConstraint(OperatorType.GREATER_THAN, "01/01/1900", "", "mm/dd/yyyy");
						validation = (XSSFDataValidation)dvHelper.CreateValidation(dvConstraint, addressList);
						validation.ShowErrorBox = true;
						validation.CreateErrorBox("Input Error", "Value must be a date, example 12/30/2000");
						sheet.AddValidationData(validation);

                  			}
               			//approval_time
			col = 21;
            cell = (XSSFCell)row.CreateCell((short)col);
			cell.CellStyle = styleDate;
		    
			if(i==startRowIndex)
			{
				
						
						addressList = new CellRangeAddressList(startRowIndex, parseRecordNumber, 21, 21);
						dvConstraint = (XSSFDataValidationConstraint)dvHelper.CreateDateConstraint(OperatorType.GREATER_THAN, "01/01/1900", "", "mm/dd/yyyy");
						validation = (XSSFDataValidation)dvHelper.CreateValidation(dvConstraint, addressList);
						validation.ShowErrorBox = true;
						validation.CreateErrorBox("Input Error", "Value must be a date, example 12/30/2000");
						sheet.AddValidationData(validation);

                  			}
                           
            }
			
            //write to byte[]
            MemoryStream ms = new MemoryStream();
            workbook.Write(ms);

            return ms.ToArray();
        }

	}
}

