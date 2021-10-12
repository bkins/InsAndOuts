using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Windows.UI.Xaml;
using InsAndOuts.Models;

namespace InsAndOuts.ViewModels
{
    public class ReportPainStoolViewModel : ViewModelBase
    {
        //Chart data
        public List<Pain>                      Pains                     { get; set; }
        public List<Stool>                     StoolTypes                { get; set; }
        public List<string>                    AvailableDateStrings      { get; set; }
        public List<DateTime>                  AvailableDateTimes        { get; set; }
        public ObservableCollection<ChartItem> PainPercentages           { get; set; }
        public ObservableCollection<ChartItem> StoolTypePercentages      { get; set; }
        public ObservableCollection<ChartItem> NumberOfStoolByDayOfWeek  { get; set; }
        public ObservableCollection<ChartItem> NumberOfStoolsByHourGroup { get; set; }

        //Chart metadata
        public string ChartTitle { get; set; }

        private List<Range> HourRanges { get; }

        public ReportPainStoolViewModel()
        {
            Pains      = DataAccessLayer.GetAllPain();
            StoolTypes = DataAccessLayer.GetAllStools();

            AvailableDateStrings = new List<string>();
            AvailableDateTimes   = new List<DateTime>();

            SetAvailablePainDateLists();
            SetAvailableStoolTypeLists();

            PainPercentages      = GetPainPercentages();
            StoolTypePercentages = GetStoolTypePercentages();

            NumberOfStoolByDayOfWeek = GetNumberOfStoolByDayOfWeek();
            
            HourRanges = GetHourRanges();

            NumberOfStoolsByHourGroup = GetNumberOfStoolsByHourGroup();
        }

        private static List<Range> GetHourRanges()
        {
            return new List<Range>
                   {
                       new Range
                       {
                           Title   = "12 AM - 4 AM"
                         , Minimum = 0
                         , Maximum = 4
                       }
                     , new Range
                       {
                           Title   = "5 AM - 9 AM"
                         , Minimum = 5
                         , Maximum = 9
                       }
                     , new Range
                       {
                           Title   = "10 AM - 2 PM"
                         , Minimum = 10
                         , Maximum = 14
                       }
                     , new Range
                       {
                           Title   = "3 PM - 6 PM"
                         , Minimum = 15
                         , Maximum = 19
                       }
                     , new Range
                       {
                           Title   = "8 PM - 12 AM"
                         , Minimum = 20
                         , Maximum = 24
                       }
                   };
        }

        private ObservableCollection<ChartItem> GetNumberOfStoolsByHourGroup()
        {
            //Has count by each hour
            var stoolTypesByHour = StoolTypes.GroupBy(fields => fields.WhenDateTime.ToString("HH"))
                                             .Select(group => new ChartItem
                                                              {
                                                                  Label = group.Key
                                                                , Value = group.Count()
                                                              })
                                             .ToList();

            var numberOfStoolByHourGroup = new List<ChartItem>();

            foreach (var stoolsInAnHour in stoolTypesByHour)
            {
                foreach (var hourRange in HourRanges.Where(hourRange => int.Parse(stoolsInAnHour.Label) >= hourRange.Minimum
                                                                    &&  int.Parse(stoolsInAnHour.Label) <= hourRange.Maximum))
                {
                    if (numberOfStoolByHourGroup.Any(field=>field.Label ==hourRange.Title))
                    {
                        numberOfStoolByHourGroup.First(field=>field.Label ==hourRange.Title).Value += stoolsInAnHour.Value;
                    }
                    else
                    {
                        numberOfStoolByHourGroup.Add(new ChartItem
                                                     {
                                                         Label                = hourRange.Title
                                                       , Value                = stoolsInAnHour.Value
                                                       , LabelUnderlyingValue = hourRange.Minimum
                                                     });
                    }
                }
            }

            return new ObservableCollection<ChartItem>(new ObservableCollection<ChartItem>(numberOfStoolByHourGroup).OrderBy(fields=>fields.LabelUnderlyingValue));
        }

        private ObservableCollection<ChartItem> GetNumberOfStoolByDayOfWeek()
        {
            var stoolTypeByDayList = StoolTypes.GroupBy(fields => fields.WhenToDateTime()
                                                                        .DayOfWeek)
                                               .Select(group => new ChartItem
                                                                {
                                                                    Label                = group.Key.ToString()
                                                                  , LabelUnderlyingValue = (int)group.Key
                                                                  , Value                = group.Count()
                                                                })
                                               .ToList();

            return new ObservableCollection<ChartItem>(new ObservableCollection<ChartItem>(stoolTypeByDayList).OrderBy(fields=>fields.LabelUnderlyingValue));
        }

        private void SetAvailableStoolTypeLists()
        {
            foreach (var stool in StoolTypes.Where(stool => ! AvailableDateStrings.Contains(stool.WhenDateTime.ToShortDateString())))
            {
                AvailableDateStrings.Add(stool.WhenDateTime.ToShortDateString());
                AvailableDateTimes.Add(stool.WhenDateTime);
            }
        }

        private void SetAvailablePainDateLists()
        {
            foreach (var pain in Pains.Where(pain => ! AvailableDateStrings.Contains(pain.WhenDateTime.ToShortDateString())))
            {
                AvailableDateStrings.Add(pain.WhenDateTime.ToShortDateString());
                AvailableDateTimes.Add(pain.WhenDateTime);
            }
        }

        private ObservableCollection<ChartItem> GetStoolTypePercentages()
        {
            var stoolTypePercentageList = StoolTypes.GroupBy(fields => fields.StoolTypeNumber)
                                                    .Select(group => new ChartItem()
                                                                     {
                                                                         Label = group.Key.ToString()
                                                                       , Value = Math.Round(group.Count() * 100 / (double)StoolTypes.Count(), 1)
                                                                     })
                                                    .ToList();
            return new ObservableCollection<ChartItem>(stoolTypePercentageList);
        }

        private ObservableCollection<ChartItem> GetPainPercentages()
        {
            var painPercentageList = Pains.GroupBy(fields => fields.Level)
                                .Select(group => new ChartItem()
                                                 {
                                                     Label = group.Key.ToString()
                                                   , Value = Math.Round(group.Count() * 100 / (double)Pains.Count(), 1)
                                                 })
                                .ToList();
            return new ObservableCollection<ChartItem>(painPercentageList);
        }

        public ReportPainStoolViewModel(string filterDate) : this()
        {
            Pains      = Pains
                        .Where(fields => fields.WhenDateTime.ToShortDateString() == filterDate).ToList();
            StoolTypes = StoolTypes
                        .Where(fields => fields.WhenDateTime.ToShortDateString() == filterDate).ToList();
        }
    }
}
