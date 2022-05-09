using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

using System;
using System.Threading.Tasks;

namespace Beamlak.Blazor.Utilities
{
    /// <summary>
    /// A class containing methods to access javascript functions for the RichTextEditor component 
    /// </summary>
    public class RichTextEditorJsInterop : IAsyncDisposable
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask;

        public RichTextEditorJsInterop(IJSRuntime jsRuntime)
        {
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
               "import", "./_content/Beamlak.Blazor.Utilities/richTextEditorJsInterop.js").AsTask());
        }

        /// <summary>
        /// Initialize and create the rich text editor element.
        /// </summary>
        /// <param name="editorElement">The reference to the markup element to use as the editor.</param>
        /// <param name="toolbar">The reference to the markup element to use as a toolbar for the editor.</param>
        /// <param name="readOnly">If set to <c>true<, the text editor will be in read-ony mode./c></param>
        /// <param name="placeholder">Initial placeholder text to use for the editor.</param>
        /// <param name="theme">The theme to use for the editor. currently supports only 'snow' and 'bubble'.</param>
        /// <returns></returns>
        public async ValueTask CreateTextEditor(ElementReference editorElement, ElementReference toolbar,
                                                         bool readOnly, string placeholder, string theme)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("createEditor", editorElement, toolbar, readOnly, placeholder, theme);
        }

        /// <summary>
        /// Get the current content of the text editor as plain text.
        /// </summary>
        /// <param name="editorElement">The reference to the text editor element.</param>
        /// <returns>The content of the editor as a text.</returns>
        public async ValueTask<string> GetEditorText(ElementReference editorElement)
        {
            var module = await moduleTask.Value;
            return await module.InvokeAsync<string>("getEditorText", editorElement);
        }

        /// <summary>
        /// Get the current content of the text editor as HTML.
        /// </summary>
        /// <param name="editorElement">The reference to the text editor element.</param>
        /// <returns>The content of the editor as HTML.</returns>
        public async ValueTask<string> GetEditorHTML(ElementReference editorElement)
        {
            var module = await moduleTask.Value;
            return await module.InvokeAsync<string>("getEditorHTML", editorElement);
        }

        /// <summary>
        /// Get the current content of the text editor as a quill delta format text.
        /// </summary>
        /// <param name="editorElement">The reference to the text editor element.</param>
        /// <returns>The content of the editor as quill delta format text.</returns>
        public async ValueTask<string> GetEditorContent(ElementReference editorElement)
        {
            var module = await moduleTask.Value;
            return await module.InvokeAsync<string>("getEditorContent", editorElement);
        }

        /// <summary>
        /// Load a quill delta format text content into the text editor.
        /// </summary>
        /// <param name="editorElement">The reference to the text editor element.</param>
        /// <param name="content">A quill delta format content to load into the editor.</param>
        /// <returns></returns>
        public async ValueTask LoadEditorContent(ElementReference editorElement, string content)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("loadEditorContent", editorElement, content);
        }

        /// <summary>
        /// Set the text editor to either editing or read-onyl mode.
        /// </summary>
        /// <param name="editorElement">The reference to the text editor element.</param>
        /// <param name="mode">If <c>false</c>, the editor will be in read-only mode.<c</param>
        /// <returns></returns>
        public async ValueTask EnableTextEditor(ElementReference editorElement, bool mode)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("enableTextEditor", editorElement, mode);
        }

        public async ValueTask DisposeAsync()
        {
            if (moduleTask.IsValueCreated)
            {
                var module = await moduleTask.Value;
                await module.DisposeAsync();
                GC.SuppressFinalize(this);
            }
        }
    }
}
