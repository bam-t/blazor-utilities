export function createEditor(
	quillElement, toolBar, readOnly,
	placeholder, theme) {
	var options = {
		modules: {
			toolbar: toolBar
		},
		placeholder: placeholder,
		readOnly: readOnly,
		theme: theme
	};
	new Quill(quillElement, options);
}

export function getEditorText(quillElement) {
	return quillElement.__quill.getText();
}

export function getEditorHTML(quillElement) {
	return quillElement.__quill.root.innerHTML;
}

export function getEditorContent(quillElement) {
	return JSON.stringify(quillElement.__quill.getContents());
}

export function loadEditorContent(quillElement, quillContent) {
	content = JSON.parse(quillContent);
	quillElement.__quill.setContents(content, 'api');
}

export function enableTextEditor(quillElement, mode) {
	quillElement.__quill.enable(mode);
}