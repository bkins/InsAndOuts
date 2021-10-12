using InsAndOuts.ViewModels;
using System;
using System.Collections.Generic;
using InsAndOuts.Views;
using Xamarin.Forms;

namespace InsAndOuts
{
    public partial class AppShell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(HomePage),          typeof(HomePage));
            Routing.RegisterRoute(nameof(MealView),          typeof(MealView));
            Routing.RegisterRoute(nameof(ConfigurationView), typeof(ConfigurationView));
            Routing.RegisterRoute(nameof(PainView),          typeof(PainView));
            Routing.RegisterRoute(nameof(StoolsView),        typeof(StoolsView));
            Routing.RegisterRoute(nameof(DailyReportView),   typeof(DailyReportView));
            Routing.RegisterRoute(nameof(ManageDataView),    typeof(ManageDataView));
            Routing.RegisterRoute(nameof(SearchView),        typeof(SearchView));
            Routing.RegisterRoute(nameof(AboutView),         typeof(AboutView));
            Routing.RegisterRoute(nameof(ReportsView),       typeof(ReportsView));
        }
        
    }
}
