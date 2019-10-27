using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using System.Threading;

namespace BetterMonacoNET
{

    /// <summary>
    /// Enum that defines available themes
    /// </summary>
    public enum MonacoTheme
    {
        Light = 0,
        Dark = 1,
        Black = 2,
    }

    public enum WhitespaceEnum
    {
        None = 0,
        Boundary = 1,
        All = 2,
    }


    public struct MonacoSettings
    {
        public bool ReadOnly;                 // Disables/Enables The Ability to Edit The Text.
        public bool AutoIndent;               // Enables auto Indentation & Adjustment
        public bool DragAndDrop;              // Enables Drag & Drop For Moving Selections of Text.
        public bool Folding;                  // Enables Code Folding.
        public bool FontLigatures;            // Enables Font Ligatures.
        public bool FormatOnPaste;            // Enables Formatting on Copy & Paste.
        public bool FormatOnType;             // Enables Formatting On Typing.
        public bool Links;                    // Enables Whether Links are clickable & detectible.
        public bool MinimapEnabled;           // Enables Whether Code Minimap is Enabled.
        public bool MatchBrackets;            // Enables Highlighting of Matching Brackets.
        public int LetterSpacing;            // Set's the Letter Spacing Between Characters.
        public int LineHeight;               // Set's the Line Height.
        public int FontSize;                 // Determine's the Font Size of the Text.
        public string FontFamily;               // Set's The Font Family for the Editor.
        public string RenderWhitespace;         // "none" | "boundary" | "all"
    };

    public partial class Monaco : Panel
    {
        public ChromiumWebBrowser chromeBrowser;
        public bool isReady = false;
        /// <summary>
        /// Starts Monaco Editor Binding
        /// </summary>
        /// <param name="MonacoEditorURL"></param>
        public void Initialize(string MonacoEditorURL = "nazism")
        {
            if (MonacoEditorURL == "nazism")
            {
                MonacoEditorURL = AppDomain.CurrentDomain.BaseDirectory + "\\Bin\\Monaco.html";
            }
            CefSettings settings = new CefSettings();
            Cef.Initialize(settings);
            chromeBrowser = new ChromiumWebBrowser(MonacoEditorURL);
            chromeBrowser.LoadingStateChanged += (sender, args) =>
            {
                //Wait for the Page to finish loading
                if (args.IsLoading == false)
                {
                    isReady = true;
                }
            };
            this.Controls.Add(chromeBrowser);
            chromeBrowser.Location = new Point(12, 36);
            chromeBrowser.Size = new Size(671, 271);
        }

        public void evento()
        {

        }

        public Action StartupFunction;

        private bool ReadOnlyObj = false;
        /// <summary>
        /// Determines Whether Monaco is Readonly
        /// </summary>
        public bool ReadOnly
        {
            get
            {
                return ReadOnlyObj;
            }
            set
            {
                ReadOnlyObj = value;
            }
        }

        private bool MinimapEnabledObj = false;
        /// <summary>
        /// Determines Whether Minimap View for Monaco is Enabled
        /// </summary>
        public bool MinimapEnabled
        {
            get
            {
                return MinimapEnabledObj;
            }
            set
            {
                MinimapEnabledObj = value;
            }
        }

        /// <summary>
        /// Editor Text
        /// </summary>
        public override string Text
        {
            get
            {
                return GetText();
            }
            set
            {
                SetText(value);
            }
        }

        private dynamic RenderWhitespaceObj = (dynamic)"none";
        /// <summary>
        /// Determines Whether the Monaco Editor will render Whitespace
        /// </summary>
        public WhitespaceEnum RenderWhitespace
        {
            get
            {
                return RenderWhitespaceObj.ToString();
            }
            set
            {
                switch (value)
                {
                    case WhitespaceEnum.None:
                        RenderWhitespaceObj = ((dynamic)"none").ToString();
                        break;
                    case WhitespaceEnum.All:
                        RenderWhitespaceObj = ((dynamic)"all").ToString();
                        break;
                    case WhitespaceEnum.Boundary:
                        RenderWhitespaceObj = ((dynamic)"boundary").ToString();
                        break;
                    default:
                        RenderWhitespaceObj = ((dynamic)"none").ToString();
                        break;
                }
            }
        }

        /// <summary>
        /// Set's Monaco Editor's Theme to the Selected Choice.
        /// </summary>
        /// <param name="theme"></param>
        public void SetTheme(MonacoTheme theme)
        {
            if (((this.chromeBrowser != null) && (isReady)) && (isReady))
            {
                switch (theme)
                {
                    case MonacoTheme.Dark:
                        chromeBrowser.ExecuteScriptAsync("SetTheme('Dark')");
                        break;
                    case MonacoTheme.Light:
                        chromeBrowser.ExecuteScriptAsync("SetTheme('Light')");
                        break;
                    case MonacoTheme.Black:
                        chromeBrowser.ExecuteScriptAsync("SetTheme('Black')");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Cannot set Monaco theme while browser is not initiated.");
            }
        }

        public void SetLanguage(string x)
        {
            if ((this.chromeBrowser != null) && (isReady))
            {
                chromeBrowser.ExecuteScriptAsync("SetLanguage", new object[] { x });
            }
            else
            {
                Console.WriteLine("Cannot set Monaco language while browser is not initiated.");
            }
        }

        /// <summary>
        /// Set's the Text of Monaco to the Parameter text.
        /// </summary>
        /// <param name="text"></param>
        public void SetText(string text)
        {
            if ((this.chromeBrowser != null) && (isReady))
            {
                chromeBrowser.ExecuteScriptAsync("SetText", new object[] { text });
            }
            else
            {
                Console.WriteLine("Cannot set Monaco's text while browser is not initiated.");
            }
        }

        /// <summary>
        /// Get's the Text of Monaco and returns it.
        /// </summary>
        /// <returns></returns>
        public string GetText()
        {
            string a = chromeBrowser.EvaluateScriptAsync("GetText").Result.ToString();
            if ((this.chromeBrowser != null) && (isReady))
            {
                var result = chromeBrowser.GetMainFrame().EvaluateScriptAsync("GetText()").Result;
                return (string)result.Result;
            }
            else
            {
                Console.WriteLine("Cannot get Monaco's text while browser is not initiated.");
                return "Cannot get Monaco's text while browser is not initiated.";
            }
        }

        /// <summary>
        /// Appends the Text of Monaco with the Parameter text.
        /// </summary>
        /// <param name="text"></param>
        public void AppendText(string text)
        {
            if ((this.chromeBrowser != null) && (isReady))
            {
                SetText(GetText() + text);
            }
            else
            {
                Console.WriteLine("Cannot append Monaco's text while browser is not initiated.");
            }
        }

        public void GoToLine(int lineNumber)
        {
            if ((this.chromeBrowser != null) && (isReady))
            {
                chromeBrowser.ExecuteScriptAsync("SetScroll", new object[] { lineNumber });
            }
            else
            {
                Console.WriteLine("Cannot go to Line in Monaco's Editor while browser is not initiated.");
            }
        }

        /// <summary>
        /// Refreshes the Monaco editor.
        /// </summary>
        public void EditorRefresh()
        {
            if ((this.chromeBrowser != null) && (isReady))
            {
                chromeBrowser.ExecuteScriptAsync("Refresh");
            }
            else
            {
                Console.WriteLine("Cannot refresh Monaco's editor while the browser is not initiated.");
            }
        }

        /// <summary>
        /// Updates Monaco Editor's Settings with it's Parameter Structure.
        /// </summary>
        /// <param name="settings"></param>
        public void UpdateSettings(MonacoSettings settings)
        {
            if ((this.chromeBrowser != null) && (isReady))
            {
                this.chromeBrowser.ExecuteScriptAsync("SwitchMinimap", new object[] { settings.MinimapEnabled });
                this.chromeBrowser.ExecuteScriptAsync("SwitchReadonly", new object[] { settings.ReadOnly });
                this.chromeBrowser.ExecuteScriptAsync("SwitchRenderWhitespace", new object[] { settings.RenderWhitespace });
                this.chromeBrowser.ExecuteScriptAsync("SwitchLinks", new object[] { settings.Links });
                this.chromeBrowser.ExecuteScriptAsync("SwitchLineHeight", new object[] { settings.LineHeight });
                this.chromeBrowser.ExecuteScriptAsync("SwitchFontSize", new object[] { settings.FontSize });
                this.chromeBrowser.ExecuteScriptAsync("SwitchFolding", new object[] { settings.Folding });
                this.chromeBrowser.ExecuteScriptAsync("SwitchAutoIndent", new object[] { settings.AutoIndent });
                this.chromeBrowser.ExecuteScriptAsync("SwitchFontFamily", new object[] { settings.FontFamily });
                this.chromeBrowser.ExecuteScriptAsync("SwitchFontLigatures", new object[] { settings.FontLigatures });
            }
            else
            {
                Console.WriteLine("Cannot change Monaco's settings while browser is not initiated.");
            }
        }

        /// <summary>
        /// Add's Intellisense for the specified Type.
        /// </summary>
        /// <param name="label"></param>
        /// <param name="type"></param>
        /// <param name="description"></param>
        /// <param name="insert"></param>
        public void AddIntellisense(string label, string type, string description, string insert)
        {
            if ((this.chromeBrowser != null) && (isReady))
            {
                chromeBrowser.ExecuteScriptAsync("AddIntellisense", new object[] {
                    label,
                    type,
                    description,
                    insert
                });
            }
            else
            {
                Console.WriteLine("Cannot edit Monaco's Intellisense while browser is not initiated.");
            }
        }

        /// <summary>
        /// Creates a Syntax Error Symbol (Squigly Red Line) on the Specific Paramaters in the Editor.
        /// </summary>
        /// <param name="line"></param>
        /// <param name="column"></param>
        /// <param name="endLine"></param>
        /// <param name="endColumn"></param>
        /// <param name="message"></param>
        public void ShowSyntaxError(int line, int column, int endLine, int endColumn, string message)
        {
            if ((this.chromeBrowser != null) && (isReady))
            {
                this.chromeBrowser.ExecuteScriptAsync("ShowErr", new object[] { line, column, endLine, endColumn, message });
            }
            else
            {
                Console.WriteLine("Cannot show Syntax Error for Monaco while browser is not initiated.");
            }
        }
    }
}