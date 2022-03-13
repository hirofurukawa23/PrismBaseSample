using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Regions;
using Prism.Services.Dialogs;

namespace PrismBaseSample.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public string Title => "メインページ";

        public string DialogText
        {
            get => _dialogText;
            set => SetProperty(ref _dialogText, value);
        }
        private string _dialogText = "";

        IDialogService _dialogService = null;

        public MainWindowViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
            DialogCommand = new DelegateCommand(DialogAction, () => true);
            SubWindowCommand = new DelegateCommand(SubWindowAction);
        }

        public DelegateCommand SubWindowCommand { get; }
        private void SubWindowAction()
        {
        }

        public DelegateCommand DialogCommand { get; }
        private void DialogAction()
        {
            ShowDialog();
        }

        private void ShowDialog()
        {
            var message = "This is a message that should be shown in the dialog.";
            //using the dialog service as-is
            _dialogService.ShowDialog("NotificationDialogView", new DialogParameters($"message={message}"), r => 
            {
                switch(r.Result)
                {
                    case ButtonResult.OK:
                        DialogText = "It's Ok!";
                        break;
                    default:
                        DialogText = "Something's Wrong!";
                        break;
                }
            });
        }
    }
}
