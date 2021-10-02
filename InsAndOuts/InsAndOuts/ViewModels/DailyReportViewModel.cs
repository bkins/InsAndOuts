using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
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
            DatesWithData = new List<string>();
            
            var allMeals  = DataAccessLayer.GetAllMeals()
                                           .OrderBy(field => DateTime.Parse(field.When));
            var allStools = DataAccessLayer.GetAllStools()
                                           .OrderBy(field => DateTime.Parse(field.When));
            var allPains  = DataAccessLayer.GetAllPain()
                                           .OrderBy(field => DateTime.Parse(field.When));

            var filterDate = dateToReportOn.ToShortDateString();
            
            SetAllItemsByFilterDate(filterDate
                                  , allMeals
                                  , allPains
                                  , allStools);

            SetDatesWithDataList(allMeals
                               , allStools
                               , allPains);
        }

        private void SetDatesWithDataList(IOrderedEnumerable<Meal>  allMeals
                                        , IOrderedEnumerable<Stool> allStools
                                        , IOrderedEnumerable<Pain>  allPains)
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

        private void SetAllItemsByFilterDate(string                    filterDate
                                           , IOrderedEnumerable<Meal>  allMeals
                                           , IOrderedEnumerable<Pain>  allPains
                                           , IOrderedEnumerable<Stool> allStools)
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

            report.AppendLine("Meals:");
            foreach (var meal in Meals)
            {
                report.AppendLine($"\t* {meal.Name} ({DateTime.Parse(meal.When).ToShortTimeString()}):");
                report.AppendLine($"\t\t{meal.DescriptionPainText}");
            }

            report.AppendLine("Stools:");

            foreach (var stool in Stools)
            {
                var hasPhoto = stool.Image == null 
                            || stool.Image.Length == 0 ?
                                       "No" :
                                       "Yes";

                report.AppendLine($"\t* {stool.StoolType.Split(':')[0]} ({DateTime.Parse(stool.When).ToShortTimeString()}):");
                report.AppendLine($"\t\t{stool.DescriptionPainText.Replace(Environment.NewLine, $"{Environment.NewLine}\t\t")}");
                report.AppendLine($"\t\tHas an photo: {hasPhoto}");
            }

            report.AppendLine("Pains:");

            foreach (var pain in Pains)
            {
                report.AppendLine($"\t* {pain.Level} ({DateTime.Parse(pain.When).ToShortTimeString()}):");
                report.AppendLine($"\t\t{pain.DescriptionPainText}");
            }

            return report.ToString();
        }
        
        public string ToHtml()
        {
            var report = new StringBuilder();
            
            report.AppendLine("<b>Meals:</b><br>");

            foreach (var meal in Meals)
            {
                report.AppendLine($"&emsp;<i>{meal.Name} ({DateTime.Parse(meal.When).ToShortTimeString()}):</i>");
                
                var description = FormatDescriptionForHtml(meal.DescriptionHtml);

                report.AppendLine($"&emsp;{description}");
            }

            report.AppendLine("<b>Stools:</b><br>");

            foreach (var stool in Stools)
            {
                var stoolType = stool.StoolType.IsNullEmptyOrWhitespace() ?
                                        "-Not specified-" :
                                        stool.StoolType.Split(':')[0];

                report.AppendLine($"&emsp;<i>{stoolType} ({DateTime.Parse(stool.When).ToShortTimeString()}):</i>");

                var hasPhoto = stool.Image        == null 
                            || stool.Image.Length == 0 ?
                                       "No" :
                                       $"Yes ({stool.ImageFileName})";
                
                var description = $"{FormatDescriptionForHtml(stool.DescriptionHtml)}<li>Has an photo: {hasPhoto}</li></ul>";

                report.AppendLine(description);
            }
            
            report.AppendLine("<b>Pains:</b><br>");

            foreach (var pain in Pains)
            {
                report.AppendLine($"&emsp;<i>Level: {pain.Level} ({DateTime.Parse(pain.When).ToShortTimeString()}):</i>");
                
                var description = $"{FormatDescriptionForHtml(pain.DescriptionHtml)}</ul>";
                
                report.AppendLine($"{description}");
            }

            return report.ToString();
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
