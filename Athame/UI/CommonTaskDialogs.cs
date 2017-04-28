using System;
using System.Diagnostics;
using System.Media;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace Athame.UI
{
    public static class CommonTaskDialogs
    {
        private const string MainCaption = "Athame";

        public static TaskDialog Wait(string message = null, IWin32Window owner = null)
        {
            var dialog = new TaskDialog
            {
                Cancelable = false,
                Caption = MainCaption,
                InstructionText = message ?? "Please wait...",
                StandardButtons = TaskDialogStandardButtons.Cancel,
                OwnerWindowHandle = owner?.Handle ?? IntPtr.Zero
            };

            var bar = new TaskDialogProgressBar { State = TaskDialogProgressBarState.Marquee };
            dialog.ProgressBar = bar;
            return dialog;
        }

        public static TaskDialog Exception(Exception exception, string errorTitle, string errorRemedy = null, IWin32Window owner = null)
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
                OwnerWindowHandle = owner?.Handle ?? IntPtr.Zero
            };
            if (Debugger.IsAttached)
            {
                var button = new TaskDialogButton("Break", "Break into debugger");
                button.Click += (sender, args) => Debugger.Break();
                td.Controls.Add(button);
            }
            var okButton = new TaskDialogButton("OK", "OK");
            okButton.Click += (sender, args) => td.Close(TaskDialogResult.Ok);
            td.Controls.Add(okButton);

            td.Opened += (sender, args) =>
            {
                td.Icon = TaskDialogStandardIcon.Error;
                SystemSounds.Hand.Play();
            };
            return td;
        }

        public static TaskDialog Message(string caption, string message, TaskDialogStandardButtons buttons = TaskDialogStandardButtons.Ok, 
            TaskDialogStandardIcon icon = TaskDialogStandardIcon.None, IWin32Window owner = null)
        {
            var td = new TaskDialog
            {
                Icon = icon,
                Caption = MainCaption,
                InstructionText = caption,
                Text = message,
                StandardButtons = buttons,
                OwnerWindowHandle = owner?.Handle ?? IntPtr.Zero
            };
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
            return td;
        }

    }
}
