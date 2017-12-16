using System;
using System.Media;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace Athame.UI
{
    public static class TaskDialogHelper
    {
        public static string MainCaption { get; set; }

        public static TaskDialog CreateWaitDialog(string message, IntPtr owner)
        {
            var dialog = new TaskDialog
            {
                Cancelable = false,
                Caption = MainCaption,
                InstructionText = message ?? "Please wait...",
                StandardButtons = TaskDialogStandardButtons.Cancel,
                OwnerWindowHandle = owner
            };

            var bar = new TaskDialogProgressBar { State = TaskDialogProgressBarState.Marquee };
            dialog.ProgressBar = bar;
            return dialog;
        }

        public static TaskDialog CreateWaitDialog(string message)
        {
            return CreateWaitDialog(message, IntPtr.Zero);
        }

        public static TaskDialog CreateExceptionDialog(Exception exception, string errorTitle, string errorRemedy)
        {
            return CreateExceptionDialog(exception, errorTitle, errorRemedy, IntPtr.Zero);
        }

        public static TaskDialog CreateExceptionDialog(Exception exception, string errorTitle, string errorRemedy, IntPtr owner)
        {
            var td = new TaskDialog
            {
                DetailsExpanded = false,
                Cancelable = true,
                Icon = TaskDialogStandardIcon.Error,
                Caption = MainCaption,
                InstructionText = errorTitle ?? "An unknown error occurred",
                Text = errorRemedy,
                DetailsExpandedLabel = "Hide details",
                DetailsCollapsedLabel = "Show details",
                DetailsExpandedText = exception.GetType().Name + ": " + exception.Message + "\n--- Begin stack trace ---\n" + exception.StackTrace,
                ExpansionMode = TaskDialogExpandedDetailsLocation.ExpandFooter,
                OwnerWindowHandle = owner
            };
            td.SetIconAndSound(TaskDialogStandardIcon.Error);
            return td;
        }

        public static TaskDialogResult ShowExceptionDialog(Exception exception, string title, string remedy,
            IntPtr owner)
        {
            return CreateExceptionDialog(exception, title, remedy, owner).Show();
        }

        public static TaskDialog CreateMessageDialog(string caption, string message, TaskDialogStandardButtons buttons,
            TaskDialogStandardIcon icon)
        {
            return CreateMessageDialog(caption, message, buttons, icon, IntPtr.Zero);
        }

        public static TaskDialog CreateMessageDialog(string caption, string message, TaskDialogStandardButtons buttons,
            TaskDialogStandardIcon icon, IntPtr owner)
        {
            var td = new TaskDialog
            {
                Caption = MainCaption,
                InstructionText = caption,
                Text = message,
                StandardButtons = buttons,
                OwnerWindowHandle = owner
            };
            td.SetIconAndSound(icon);
            return td;
        }

        public static TaskDialogResult ShowMessage(string caption, string message)
        {
            return CreateMessageDialog(caption, message, TaskDialogStandardButtons.Ok, TaskDialogStandardIcon.None, IntPtr.Zero).Show();
        }

        public static TaskDialogResult ShowMessage(string caption, string message, TaskDialogStandardButtons buttons)
        {
            return CreateMessageDialog(caption, message, buttons, TaskDialogStandardIcon.None, IntPtr.Zero).Show();
        }

        public static TaskDialogResult ShowMessage(string caption, string message, TaskDialogStandardButtons buttons,
            TaskDialogStandardIcon icon)
        {
            return CreateMessageDialog(caption, message, buttons, icon, IntPtr.Zero).Show();
        }

        public static TaskDialogResult ShowMessage(string caption, string message, TaskDialogStandardButtons buttons,
            TaskDialogStandardIcon icon, IntPtr owner)
        {
            return CreateMessageDialog(caption, message, buttons, icon, owner).Show();
        }

        public static void SetIconAndSound(this TaskDialog td, TaskDialogStandardIcon icon)
        {
            td.Opened += (sender, args) =>
            {
                td.Icon = icon;
                switch (icon)
                {
                    case TaskDialogStandardIcon.None:
                        break;
                    case TaskDialogStandardIcon.Warning:
                        SystemSounds.Exclamation.Play();
                        break;
                    case TaskDialogStandardIcon.Error:
                        SystemSounds.Hand.Play();
                        break;
                    case TaskDialogStandardIcon.Information:
                        SystemSounds.Asterisk.Play();
                        break;
                    case TaskDialogStandardIcon.Shield:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(icon), icon, null);
                }
            };
        }

    }
}
