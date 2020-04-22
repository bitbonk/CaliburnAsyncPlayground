namespace CaliburnAsyncPlayground
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Caliburn.Micro;

    public class ShellViewModel : Conductor<Screen>.Collection.OneActive
    {
        private readonly IEventAggregator eventAggregator;

        public ShellViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
            this.DisplayName = "Caliburn.Micro Alpha Test";
        }

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            this.Items.Add(new SubViewModel(this.eventAggregator) { DisplayName = App.FirstScreen });
            this.Items.Add(new SubViewModel(this.eventAggregator) { DisplayName = App.SecondScreen });
            return Task.CompletedTask;
        }
    }
}