using System.ComponentModel.Composition;

using AboutModule.Events;

using MessageBox.Contracts;

using Microsoft.Practices.Prism.Events;

namespace AboutModule.Controller
{
    [Export]
    public class AboutContentController
    {
        #region Constants and Fields

        private readonly IEventAggregator eventAggregator;

        private readonly IMessageBoxService messageBoxService;

        #endregion

        #region Constructors and Destructors

        [ImportingConstructor]
        public AboutContentController(IEventAggregator eventAggregator, IMessageBoxService messageBoxService)
        {
            this.eventAggregator = eventAggregator;
            this.messageBoxService = messageBoxService;

            this.eventAggregator.GetEvent<ShowAboutEvent>().Subscribe(this.ShowAbout, true);
        }

        #endregion

        #region Public Methods

        public void ShowAbout(object obj)
        {
            this.messageBoxService.ShowMessage("Prism 4.0 Example Application, JB");
        }

        #endregion
    }
}