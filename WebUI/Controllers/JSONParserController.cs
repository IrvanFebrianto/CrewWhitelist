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
using HtmlAgilityPack;
using System.Text;

namespace WebUI.Controllers
{
    public class JSONParserController : MyController
    {
		private ILogRepository RepoLog;

        public JSONParserController(ILogRepository repoLog)
			: base(repoLog)
        {

        }

        public ActionResult Index()
        {
            return View();
        }

        public string Parse(string url)
        {
           
            url = Url.Action("Index" ,null, null, Request.Url.Scheme);

            HtmlNode.ElementsFlags.Remove("form");
            HtmlNode.ElementsFlags.Remove("option");
            HtmlNode.ElementsFlags.Remove("div");

            HtmlWeb htmlWeb = new HtmlWeb();
            HtmlDocument htmlDocument = htmlWeb.Load(url);

            HtmlNode formNode = htmlDocument.DocumentNode.SelectNodes("//form")[0];

            List<RowModel> model = new List<RowModel>();
            IEnumerable<HtmlNode> nodeCollection = formNode.Descendants().Where(x => x.Attributes["class"] != null);
            nodeCollection = nodeCollection.Where(x => x.Attributes["class"].Value == "row");
            //IEnumerable<HtmlNode> links = formNode.;

            int i = 1;
            foreach(HtmlNode data in nodeCollection)
            {
                ProcessRow(data, ref model,i);
                i++;
            }

            return new JavaScriptSerializer().Serialize(model);
        }

        private void ProcessRow(HtmlNode formNode, ref List<RowModel> rowModel, int rowNumber)
        {
            List<ParseModel> model = new List<ParseModel>();
            IEnumerable<HtmlNode> nodeCollection = formNode.Descendants().Where(x => x.Name == "input" ||
                x.Name == "select" ||
                x.Name == "textarea" ||
                x.Name == "table" ||
                x.Name == "label").Where(x => x.Attributes["data-table-input"] == null);
            //IEnumerable<HtmlNode> links = formNode.;

            foreach (HtmlNode data in nodeCollection)
            {
                ProcessData(data, ref model);
            }
            RowModel dataRow = new RowModel();
            dataRow.Content = model;
            dataRow.RowNumber = rowNumber;

            rowModel.Add(dataRow);
        }

        private void ProcessData(HtmlNode data, ref List<ParseModel> model)
        {
            ParseModel parseData;
            if (data.Name == "input")
            {
                switch (data.Attributes["type"].Value)
                {
                    case "text":
                        parseData = new ParseModel();
                        parseData.Type = "text";
                        parseData.Name = (data.Attributes["name"] != null) ? data.Attributes["name"].Value : null;
                        parseData.Id = (data.Attributes["id"] != null) ? data.Attributes["id"].Value : null;
                        parseData.Value = (data.Attributes["value"] != null) ? data.Attributes["value"].Value : null;
                        parseData.ReadOnly = (data.Attributes["readonly"] != null) ? true : false;
                        parseData.Disabled = (data.Attributes["disabled"] != null) ? true : false;

                        model.Add(parseData);
                        break;
                    case "password":
                        parseData = new ParseModel();
                        parseData.Type = "password";
                        parseData.Name = (data.Attributes["name"] != null) ? data.Attributes["name"].Value : null;
                        parseData.Id = (data.Attributes["id"] != null) ? data.Attributes["id"].Value : null;
                        parseData.Value = (data.Attributes["value"] != null) ? data.Attributes["value"].Value : null;
                        parseData.ReadOnly = (data.Attributes["readonly"] != null) ? true : false;
                        parseData.Disabled = (data.Attributes["disabled"] != null) ? true : false;

                        model.Add(parseData);
                        break;
                    case "date":
                        parseData = new ParseModel();
                        parseData.Type = "date";
                        parseData.Name = (data.Attributes["name"] != null) ? data.Attributes["name"].Value : null;
                        parseData.Id = (data.Attributes["id"] != null) ? data.Attributes["id"].Value : null;
                        parseData.Value = (data.Attributes["value"] != null) ? data.Attributes["value"].Value : null;
                        parseData.ReadOnly = (data.Attributes["readonly"] != null) ? true : false;
                        parseData.Disabled = (data.Attributes["disabled"] != null) ? true : false;

                        model.Add(parseData);
                        break;
                    case "hidden":
                        parseData = new ParseModel();
                        parseData.Type = "hidden";
                        parseData.Name = (data.Attributes["name"] != null) ? data.Attributes["name"].Value : null;
                        parseData.Id = (data.Attributes["id"] != null) ? data.Attributes["id"].Value : null;
                        parseData.Value = (data.Attributes["value"] != null) ? data.Attributes["value"].Value : null;
                        parseData.ReadOnly = (data.Attributes["readonly"] != null) ? true : false;
                        parseData.Disabled = (data.Attributes["disabled"] != null) ? true : false;

                        model.Add(parseData);
                        break;
                    case "radio":
                        var prevData = (from a in model
                                     where a.RadioGroupModel != null &&
                                     a.RadioGroupModel.Count > 0 &&
                                     a.Name == data.Attributes["name"].Value
                                     select a).FirstOrDefault();
                        if(prevData==null)
                        {
                            parseData = new ParseModel();
                            parseData.Type = "radioGroup";
                            parseData.Name = (data.Attributes["name"] != null) ? data.Attributes["name"].Value : null;

                            RadioGroupModel radioButtonData = new RadioGroupModel();

                            radioButtonData.Id = (data.Attributes["id"] != null) ? data.Attributes["id"].Value : null;
                            radioButtonData.Value = (data.Attributes["value"] != null) ? data.Attributes["value"].Value : null;
                            radioButtonData.InnerLabel = data.NextSibling.InnerHtml.Replace(System.Environment.NewLine, string.Empty).Trim();
                            radioButtonData.ReadOnly = (data.Attributes["readonly"] != null) ? true : false;
                            radioButtonData.Disabled = (data.Attributes["disabled"] != null) ? true : false;
                            radioButtonData.Checked = (data.Attributes["checked"] != null) ? true : false;

                            parseData.RadioGroupModel = new List<RadioGroupModel>();
                            parseData.RadioGroupModel.Add(radioButtonData);
                            
                            model.Add(parseData);
                            
                        }
                        else
                        {
                            RadioGroupModel radioButtonData = new RadioGroupModel();

                            radioButtonData.Id = (data.Attributes["id"] != null) ? data.Attributes["id"].Value : null;
                            radioButtonData.Value = (data.Attributes["value"] != null) ? data.Attributes["value"].Value : null;
                            radioButtonData.InnerLabel = data.NextSibling.InnerHtml.Replace(System.Environment.NewLine, string.Empty).Trim();
                            radioButtonData.ReadOnly = (data.Attributes["readonly"] != null) ? true : false;
                            radioButtonData.Disabled = (data.Attributes["disabled"] != null) ? true : false;
                            radioButtonData.Checked = (data.Attributes["checked"] != null) ? true : false;

                            prevData.RadioGroupModel.Add(radioButtonData);
                        }
                        break;
                    case "checkbox":
                        //parseData = new ParseModel();
                        //parseData.Type = "checkbox";
                        //parseData.Name = (data.Attributes["name"] != null) ? data.Attributes["name"].Value : null;
                        //parseData.Id = (data.Attributes["id"] != null) ? data.Attributes["id"].Value : null;
                        //parseData.Value = (data.Attributes["value"] != null) ? data.Attributes["value"].Value : null;
                        //parseData.InnerLabel = data.NextSibling.InnerHtml.Replace(System.Environment.NewLine, string.Empty).Trim();
                        //parseData.ReadOnly = (data.Attributes["readonly"] != null) ? true : false;
                        //parseData.Disabled = (data.Attributes["disabled"] != null) ? true : false;
                        //parseData.Checked = (data.Attributes["checked"] != null) ? true : false;
                        //model.Add(parseData);
                        var prevDataCheckbox = (from a in model
                                     where a.CheckBoxListModel != null &&
                                     a.CheckBoxListModel.Count > 0 &&
                                     a.Name == data.Attributes["name"].Value
                                     select a).FirstOrDefault();
                        if(prevDataCheckbox==null)
                        {
                            parseData = new ParseModel();
                            parseData.Type = "checkBoxList";
                            parseData.Name = (data.Attributes["name"] != null) ? data.Attributes["name"].Value : null;

                            CheckBoxListModel checkBoxListData = new CheckBoxListModel();

                            checkBoxListData.Id = (data.Attributes["id"] != null) ? data.Attributes["id"].Value : null;
                            checkBoxListData.Value = (data.Attributes["value"] != null) ? data.Attributes["value"].Value : null;
                            checkBoxListData.InnerLabel = data.NextSibling.InnerHtml.Replace(System.Environment.NewLine, string.Empty).Trim();
                            checkBoxListData.ReadOnly = (data.Attributes["readonly"] != null) ? true : false;
                            checkBoxListData.Disabled = (data.Attributes["disabled"] != null) ? true : false;
                            checkBoxListData.Checked = (data.Attributes["checked"] != null) ? true : false;

                            parseData.CheckBoxListModel = new List<CheckBoxListModel>();
                            parseData.CheckBoxListModel.Add(checkBoxListData);
                            
                            model.Add(parseData);
                            
                        }
                        else
                        {
                            CheckBoxListModel checkBoxListData = new CheckBoxListModel();

                            checkBoxListData.Id = (data.Attributes["id"] != null) ? data.Attributes["id"].Value : null;
                            checkBoxListData.Value = (data.Attributes["value"] != null) ? data.Attributes["value"].Value : null;
                            checkBoxListData.InnerLabel = data.NextSibling.InnerHtml.Replace(System.Environment.NewLine, string.Empty).Trim();
                            checkBoxListData.ReadOnly = (data.Attributes["readonly"] != null) ? true : false;
                            checkBoxListData.Disabled = (data.Attributes["disabled"] != null) ? true : false;
                            checkBoxListData.Checked = (data.Attributes["checked"] != null) ? true : false;

                            prevDataCheckbox.CheckBoxListModel.Add(checkBoxListData);

                        }
                        
                        break;
                }
            }
            else if(data.Name == "label")
            {
                parseData = new ParseModel();
                parseData.Type = "label";
                parseData.Name = (data.Attributes["name"] != null) ? data.Attributes["name"].Value : null;
                parseData.Id = (data.Attributes["id"] != null) ? data.Attributes["id"].Value : null;
                parseData.Value = data.InnerHtml;
                parseData.ReadOnly = (data.Attributes["readonly"] != null) ? true : false;
                parseData.Disabled = (data.Attributes["disabled"] != null) ? true : false;
                parseData.LabelFor = (data.Attributes["for"] != null) ? data.Attributes["for"].Value : null;

                model.Add(parseData);
            }
            else if (data.Name == "textarea")
            {
                parseData = new ParseModel();
                parseData.Type = "textarea";
                parseData.Name = (data.Attributes["name"] != null) ? data.Attributes["name"].Value : null;
                parseData.Id = (data.Attributes["id"] != null) ? data.Attributes["id"].Value : null;
                parseData.Value = data.InnerHtml;
                parseData.ReadOnly = (data.Attributes["readonly"] != null) ? true : false;
                parseData.Disabled = (data.Attributes["disabled"] != null) ? true : false;

                model.Add(parseData);
            }
            else if (data.Name == "select")
            {
                parseData = new ParseModel();
                parseData.Type = "select";
                parseData.Name = (data.Attributes["name"] != null) ? data.Attributes["name"].Value : null;
                parseData.Id = (data.Attributes["id"] != null) ? data.Attributes["id"].Value : null;
                parseData.Value = null;
                parseData.ReadOnly = (data.Attributes["readonly"] != null) ? true : false;
                parseData.Disabled = (data.Attributes["disabled"] != null) ? true : false;


                var optionValues = new List<SelectListItem>();
                var optionList = data.SelectNodes("//option");



                foreach (HtmlNode option in optionList)
                {
                    SelectListItem optionItem = new SelectListItem();
                    optionItem.Text = option.InnerText;
                    optionItem.Value = (option.Attributes["value"] != null) ? option.Attributes["value"].Value : null;
                    optionItem.Selected = (option.Attributes["selected"] != null) ? true : false;

                    optionValues.Add(optionItem);
                }
                parseData.OptionValue = optionValues;

                model.Add(parseData);
            }
            else if (data.Name == "table")
            {
                parseData = new ParseModel();
                parseData.Type = "table";
                parseData.Name = (data.Attributes["name"] != null) ? data.Attributes["name"].Value : null;
                parseData.Id = (data.Attributes["id"] != null) ? data.Attributes["id"].Value : null;
                parseData.Value = (data.Attributes["value"] != null) ? data.Attributes["value"].Value : null;
                parseData.ReadOnly = (data.Attributes["readonly"] != null) ? true : false;
                parseData.Disabled = (data.Attributes["disabled"] != null) ? true : false;

                List<TableRowModel> rowList = new List<TableRowModel>();

                IEnumerable<HtmlNode> rowCollection = data.Descendants().Where(x => x.Name == "tr");
                int i = 1;
                foreach (var rw in rowCollection)
                {
                    TableRowModel row = new TableRowModel();
                    row.RowNumber = i;
                    List<TableColumnModel> columnList = new List<TableColumnModel>();

                    IEnumerable<HtmlNode> columnCollection = rw.Descendants().Where(x => x.Name == "td" || x.Name == "th");
                    int j = 1;
                    foreach (var cl in columnCollection)
                    {
                        TableColumnModel column = new TableColumnModel();
                        column.ColumnNumber = j;
                        if (cl.Name == "th")
                        {
                            column.IsHeader = true;
                            column.Value = cl.InnerHtml;
                        }
                        else
                        {
                            column.IsHeader = false;

                            IEnumerable<HtmlNode> columnDescendants = cl.Descendants();
                            foreach(var u in columnDescendants)
                            {
                                if (u.Name == "#text")
                                {
                                    column.Value = cl.InnerHtml;
                                }
                                else
                                {
                                    List<ParseModel> columnData = new List<ParseModel>();
                                        //IEnumerable<HtmlNode> columContentCollection = u.Descendants().Where(x => x.Name == "input" ||
                                        //x.Name == "select" ||
                                        //x.Name == "textarea" ||
                                        //x.Name == "table");
                                        //foreach(var v in columnCollection)
                                        //{
                                        //    ProcessData(u, ref columnData);
                                        //}
                                        ProcessData(u, ref columnData); 
                                        column.ParseModel = columnData;
                                }
                            }
                        }
                        columnList.Add(column);
                        j++;
                    }
                    row.TableColumnModel = columnList;
                    rowList.Add(row);

                    i++;

                }

                parseData.TableRowModel = rowList;
                model.Add(parseData);
            }
                
        }

	}

    public class RowModel
    {
        public int RowNumber { get; set; }
        public List<ParseModel> Content { get; set; }
    }

    public class ParseModel
    {
        public string Type { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        public bool ReadOnly { get; set; }

        public bool Disabled { get; set; }

        public bool Checked { get; set; }

        public string InnerLabel { get; set; }

        public string LabelFor { get; set; }

        public List<SelectListItem> OptionValue { get; set; }

        public List<TableRowModel> TableRowModel { get; set; }

        public List<RadioGroupModel> RadioGroupModel { get; set; }

        public List<CheckBoxListModel> CheckBoxListModel { get; set; }
    }

    public class TableRowModel
    {
        public int RowNumber { get; set; }
        public List<TableColumnModel> TableColumnModel { get; set; }

    }

    public class TableColumnModel
    {
        public int ColumnNumber { get; set; }
        public bool IsHeader { get; set; }
        public string Value { get; set; }
        public List<ParseModel> ParseModel { get; set; }
    }

    public class RadioGroupModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public bool ReadOnly { get; set; }
        public bool Disabled { get; set; }
        public bool Checked { get; set; }
        public string InnerLabel { get; set; }
    }

    public class CheckBoxListModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public bool ReadOnly { get; set; }
        public bool Disabled { get; set; }
        public bool Checked { get; set; }
        public string InnerLabel { get; set; }
    }
}

