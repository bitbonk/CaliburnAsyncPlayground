namespace CaliburnAsyncPlayground
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Caliburn.Micro;
    using Serilog;

    public class SubViewModel : Screen, IHandle<Message>
    {
        private readonly IEventAggregator eventAggregator;
        private int activationCounter;
        private int commandCounter;
        private int receiveCounter;
        private int sendCounter;

        public SubViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
        }

        public async Task SendSomethingAsync(CancellationToken cancellationToken)
        {
            this.sendCounter++;
            Log.Information(
                $"{this.sendCounter} Sending message number {this.sendCounter} from '{this.DisplayName}' {cancellationToken.IsCancellationRequested}");
            await this.eventAggregator.PublishOnCurrentThreadAsync(
                new Message
                {
                    For = this.DisplayName == App.FirstScreen ? App.SecondScreen : App.FirstScreen,
                    Text = this.sendCounter.ToString()
                },
                cancellationToken);
            Log.Information(
                $"{this.sendCounter} Sent message number {this.sendCounter} from '{this.DisplayName}' {cancellationToken.IsCancellationRequested}");
        }

        public async Task DoSomethingAsync(CancellationToken cancellationToken)
        {
            this.commandCounter++;
            Log.Information(
                $"{this.commandCounter} Calling DomeSomething on '{this.DisplayName}' {cancellationToken.IsCancellationRequested}");
            await Task.Delay(App.TaskDelay, cancellationToken);
            Log.Information(
                $"{this.commandCounter} Called DoSomething on  '{this.DisplayName}' {cancellationToken.IsCancellationRequested}");
            this.commandCounter--;
        }

        public async Task HandleAsync(Message message, CancellationToken cancellationToken)
        {
            if (message.For != this.DisplayName)
            {
                return;
            }

            this.receiveCounter++;
            Log.Information(
                $"{this.receiveCounter} Handling message number {message.Text} in '{this.DisplayName}' {cancellationToken.IsCancellationRequested}");
            await Task.Delay(App.TaskDelay, cancellationToken);
            Log.Information(
                $"{this.receiveCounter} Handled message number {message.Text} in '{this.DisplayName}' {cancellationToken.IsCancellationRequested}");
        }

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            this.eventAggregator.SubscribeOnPublishedThread(this);
            return Task.CompletedTask;
        }

        protected override async Task OnActivateAsync(CancellationToken cancellationToken)
        {
            this.activationCounter++;
            Log.Information(
                $"{this.activationCounter} Activating '{this.DisplayName}' {cancellationToken.IsCancellationRequested}");
            await Task.Delay(App.TaskDelay, cancellationToken);
            Log.Information(
                $"{this.activationCounter} Activated '{this.DisplayName}' {cancellationToken.IsCancellationRequested}");
        }

        protected override async Task OnDeactivateAsync(bool close, CancellationToken cancellationToken)
        {
            Log.Information(
                $"{this.activationCounter} Deactivating '{this.DisplayName}' {cancellationToken.IsCancellationRequested}");
            await Task.Delay(App.TaskDelay, cancellationToken);
            Log.Information(
                $"{this.activationCounter} Deactivated '{this.DisplayName}' {cancellationToken.IsCancellationRequested}");
            this.activationCounter--;
        }
    }
}