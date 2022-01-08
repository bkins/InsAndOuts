using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using InsAndOuts.Data;
using InsAndOuts.Models;
using InsAndOuts.Utilities;

namespace InsAndOuts.ViewModels
{
    public class DailyReportViewModel : ViewModelBase
    {
        public List<Meal>   Meals         { get; set; }
        public List<Pain>   Pains         { get; set; }
        public List<Stool>  Stools        { get; set; }
        public List<string> DatesWithData { get; set; }

        public DailyReportViewModel(DateTime dateToReportOn)
        {
            BuildDailyReport(dateToReportOn);
        }

        private void BuildDailyReport(DateTime dateToReportOn)
        {
            DatesWithData = new List<string>();

            Meals = DataAccessLayer.GetAllMeals();
            var allMeals = Meals
                          .Where(field => GetShortDateFromString(field.When) == dateToReportOn.ToShortDateString())
                          .OrderBy(field => DateTime.Parse(field.When));

            Stools = DataAccessLayer.GetAllStools();
            var allStools = Stools
                           .Where(field => GetShortDateFromString(field.When) == dateToReportOn.ToShortDateString())
                           .OrderBy(field => DateTime.Parse(field.When));

            Pains = DataAccessLayer.GetAllPain();
            var allPains = Pains
                          .Where(field => GetShortDateFromString(field.When) == dateToReportOn.ToShortDateString())
                          .OrderBy(field => DateTime.Parse(field.When));

            var filterDate = dateToReportOn.ToShortDateString();

            //SetAllItemsByFilterDate(filterDate
            //                      , allMeals
            //                      , allPains
            //                      , allStools);

            SetDatesWithDataList(allMeals
                               , allStools
                               , allPains);
        }

        public DailyReportViewModel(DateTime dateToReportOn, bool useTestData = false, IDataStore database = null)
        {
            if (useTestData)
            {
                DataAccessLayer = new DataAccess(database);
            }

            BuildDailyReport(dateToReportOn);
        }

        private void SetDatesWithDataList(IEnumerable<Meal>  allMeals
                                        , IEnumerable<Stool> allStools
                                        , IEnumerable<Pain>  allPains)
        {
            foreach (var meal in allMeals.Where(meal => meal.Id != 0 && ! DatesWithData.Contains(meal.When)))
            {
                DateTime.TryParse(meal.When, out var outDate);
                var date = outDate.ToShortDateString();

                if ( ! DatesWithData.Contains(date))
                {
                    DatesWithData.Add(outDate.ToShortDateString());
                }
            }

            foreach (var stool in allStools.Where(stool => stool.Id != 0 && ! DatesWithData.Contains(stool.When)))
            {
                DateTime.TryParse(stool.When, out var outDate);
                var date = outDate.ToShortDateString();

                if ( ! DatesWithData.Contains(date))
                {
                    DatesWithData.Add(outDate.ToShortDateString());
                }
            }

            foreach (var pain in allPains.Where(pain => pain.Id != 0))
            {
                DateTime.TryParse(pain.When, out var outDate);
                var date = outDate.ToShortDateString();

                if ( ! DatesWithData.Contains(date))
                {
                    DatesWithData.Add(outDate.ToShortDateString());
                }
            }
        }

        private void SetAllItemsByFilterDate(string             filterDate
                                           , IEnumerable<Meal>  allMeals
                                           , IEnumerable<Pain>  allPains
                                           , IEnumerable<Stool> allStools)
        {
            Meals = allMeals.Where(field => GetShortDateFromString(field.When) == filterDate)
                            .ToList();

            Pains = allPains.Where(field => GetShortDateFromString(field.When) == filterDate)
                            .ToList();

            Stools = allStools.Where(field => GetShortDateFromString(field.When) == filterDate)
                              .ToList();
        }

        public string ToPainText()
        {
            var report = new StringBuilder();
            
            if (Meals != null)
            {
                BuildMealsReport(report);
            }

            if (Stools != null)
            {
                BuildStoolsReport(report);
            }

            if (Pains != null)
            {
                BuildPainsReport(report);
            }

            return report.ToString();
        }

        private void BuildPainsReport(StringBuilder report)
        {
            report.AppendLine("Pains:");
            report.AppendLine("");

            foreach (var pain in Pains)
            {
                report.AppendLine($"* {pain.Level} ({DateTime.Parse(pain.When).ToShortTimeString()}):");

                if (pain.DescriptionPainText.HasValue())
                {
                    report.AppendLine($"  {pain.DescriptionPainText.Trim().Replace(Environment.NewLine, $"{Environment.NewLine}  ")}");
                }
            }
        }

        private void BuildStoolsReport(StringBuilder report)
        {
            report.AppendLine("");
            report.AppendLine("Stools:");
            report.AppendLine("");

            foreach (var stool in Stools)
            {
                var hasPhoto = stool.Image == null || stool.Image.Length == 0 ?
                                       "No" :
                                       $"Yes ({stool.ImageFileName})";

                report.AppendLine($"* {stool.StoolType.Split(':')[0]} ({DateTime.Parse(stool.When).ToShortTimeString()}):");

                if (stool.DescriptionPainText.HasValue())
                {
                    report.AppendLine($"  {stool.DescriptionPainText.Trim().Replace(Environment.NewLine, $"{Environment.NewLine}  ")}");
                }

                report.AppendLine($"  Has an photo: {hasPhoto}");
                
                report.AppendLine("");
            }
        }

        private void BuildMealsReport(StringBuilder report)
        {
            report.AppendLine("Meals:");

            foreach (var meal in Meals)
            {
                report.AppendLine("");
                var mealName                = meal.Name;
                var mealWhen                = meal.When;
                var mealWhenDateTime        = DateTime.Parse(mealWhen);
                var mealWhenShortTimeString = mealWhenDateTime.ToShortTimeString();

                report.AppendLine($"* {mealName} ({mealWhenShortTimeString}):");

                if (meal.DescriptionPainText.HasValue())
                {
                    report.AppendLine($"  {meal.DescriptionPainText.Trim().Replace(Environment.NewLine, $"{Environment.NewLine}  ")}");
                }
            }
        }

        public string ToHtml()
        {
            var report = new StringBuilder();

            if (Meals != null)
            {
                BuildMealsHtmlReport(report);
            }

            if (Stools != null)
            {
                BuildStoolsHtmlReport(report);
            }

            if (Pains != null)
            {
                BuildPainsHtmlReport(report);
            }

            return report.ToString();
        }

        private void BuildPainsHtmlReport(StringBuilder report)
        {
            report.AppendLine("<b>Pains:</b><br>");

            foreach (var pain in Pains)
            {
                report.AppendLine($"&emsp;<i>Level: {pain.Level} ({DateTime.Parse(pain.When).ToShortTimeString()}):</i>");

                var description = $"{FormatDescriptionForHtml(pain.DescriptionHtml)}</ul>";

                report.AppendLine($"{description}");
            }
        }

        private void BuildStoolsHtmlReport(StringBuilder report)
        {
            report.AppendLine("<b>Stools:</b><br>");

            foreach (var stool in Stools)
            {
                var stoolType = stool.StoolType.IsNullEmptyOrWhitespace() ?
                                        "-Not specified-" :
                                        stool.StoolType.Split(':')[0];

                report.AppendLine($"&emsp;<i>{stoolType} ({DateTime.Parse(stool.When).ToShortTimeString()}):</i>");

                var hasPhoto = stool.Image == null || stool.Image.Length == 0 ?
                                       "No" :
                                       $"Yes ({stool.ImageFileName})";

                var description = $"{FormatDescriptionForHtml(stool.DescriptionHtml)}<li>Has an photo: {hasPhoto}</li></ul>";

                report.AppendLine(description);
            }
        }

        private void BuildMealsHtmlReport(StringBuilder report)
        {
            report.AppendLine("<b>Meals:</b><br>");

            foreach (var meal in Meals)
            {
                report.AppendLine($"&emsp;<i>{meal.Name} ({DateTime.Parse(meal.When).ToShortTimeString()}):</i>");

                var description = FormatDescriptionForHtml(meal.DescriptionHtml);

                report.AppendLine($"&emsp;{description}");
            }
        }

        private static string FormatDescriptionForHtml(string description)
        {
            description = description
                         .Replace("<p>",  "")
                         .Replace("</p>", "");

            if (description.IsNullEmptyOrWhitespace())
            {
                return "<ul>";
            }

            return description.Contains("<ul><li>") ?
                           description :
                           $"<ul><li>{description}</li>";
        }

        private string GetShortDateFromString(string date)
        {
            if (DateTime.TryParse(date, out var returnDate))
            {
                return returnDate.ToShortDateString();
            }

            return date;

        }

    }
}
