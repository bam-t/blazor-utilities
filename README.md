# Blazor Utilities

Blazor Utilities is a collection of reusable components and classes for Blazor Server and Blazor Web Assembly applications. It is not a complete project, and new components and utility classes are actively being added to it.


### Contents

This project currently contains the following blazor components and utility classes:

1. RichTextEditor
2. BrowserStorage

### 1. RichTextEditor

When I started learning blazor (webassembly), I ran into a situation where I wanted to display a formatted text like ***rtf*** text on my pages and components, but I had a hard time finding an open source, ready to use, and easily customizable rich text editor component. So I decided to create my own component.
Since I was starting to learn blazor myself, I decided to start from an existing projet, so this component is inspired by a project by [erossini](https://github.com/erossini/BlazorQuillComponent).<br>

###### What is Quill?

[Quill](https://quilljs.com/) is an modern, [open source](https://github.com/quilljs/quill/) javascript rich text editor built for compatibility and extensibility. You can view Quill's documentation to furthur costomize this component.

###### Features of the RichTextEditor

- Loading a Quill Delta format text
- Get content of editor as plain text, html or Quill Delta format text
- Read-only mode (suitable to display rich formatted texts that don't need editing)
- Inline mode editing
- Customizable toolbar contents (Predefined and user specified)
- Custome themes (Light, Dark, and Inherit modes at the monemt)

#### How to use

Install the nuget package using the package manager:
> Install-Package beamlak.blazor.utilities

Or you can install using visual studio package manager.

Once you install the package, add the following references to your application:
- In the `head` tag of your `Pages\\_Layout.cshtml` (.NET 6) or `Pages\\_Host.cshtml` (.NET 5) if you're using Blazor Server, or `wwwroot\\index.html` if your're using Blazor Webassembly, add the following CSS:
```html
<link href="//cdn.quilljs.com/1.3.6/quill.snow.css" rel="stylesheet">
<link href="//cdn.quilljs.com/1.3.6/quill.bubble.css" rel="stylesheet">
```
- And at the bottom of the file, add the following javascript reference:<br>
```html
<script src="https://cdn.quilljs.com/1.3.6/quill.js"></script>
```
- Add the javascript interop service as a dependency in your **Program.cs** file.
```cs
using Beamlak.Blazor.Utilities

builder.Services.AddScoped<RichTextEditorJsInterop>();
```

Now you can use the editor in your app:
```razor
@using Beamlak.Blazor.Utilities.Components

<RichTextEditor>
    <EditorContent>
        <h1>Hello Rich Text World!</h1>
    </EditorContent>
</RichTextEditor>
```
![basic-editor](https://user-images.githubusercontent.com/53230475/167357164-818e0b5b-afbf-40b7-bfc4-01505a41e9b0.png)

#### Editor Modes

The editor has 3 modes:
  - Normal Mode (default): The editor is displayed with the toolbar and border.
  - Inline Mode: The toolbar and border will not be visible, but the content is still editable.
  - Readonly Mode: The toolbar and border will not be visible, and the content is not editable.

```razor
@using Beamlak.Blazor.Utilities.Components
@using static Beamlak.Blazor.Utilities.Enums

You can edit the text below:
<br />
<RichTextEditor EditorMode="EditorMode.Inline" 
                ToolbarOption="ToolbarOption.Full">
    <EditorContent>
        <h1>Hello Rich Text World!</h1>
    </EditorContent>
</RichTextEditor>
```
![inline-mode](https://user-images.githubusercontent.com/53230475/167367637-eb612d17-7df0-4793-b706-c3b910dd3d68.png)

#### Toolbar Options

You can chose from 2 pre-defined toolbar options:
  - Full: A full set of toolbar controlls will be displayed.
  - Minimal (default): A minimal set of toolbar controlls will be displayed.

You can also customize the toolbar by supplying your own markup, wich will override the default toolbar contents:

```razor
@using Beamlak.Blazor.Utilities.Components

<RichTextEditor>
    <ToolbarContent>
        <span class="ql-formats">
            <select class="ql-font"></select>
            <select class="ql-size"></select>
        </span>
        <span class="ql-formats">
            <button class="ql-bold"></button>
            <button class="ql-italic"></button>
            <button class="ql-underline"></button>
            <button class="ql-strike"></button>
        </span>
    </ToolbarContent>
    <EditorContent>
        <h1>Hello Rich Text World!</h1>
    </EditorContent>
</RichTextEditor>
```
![custom-toolbar](https://user-images.githubusercontent.com/53230475/167359041-a30ef37b-d629-4491-942c-3dc661acd383.png)

If you want to control the size of the editor, wrap the editor in a container `div` element and set the size of the container:
```razor
@using Beamlak.Blazor.Utilities.Components

<div style="width: 75%">
    <RichTextEditor>
        <EditorContent>
            <h1>Hello Rich Text World!</h1>
        </EditorContent>
    </RichTextEditor>
</div>
```

You can load the content of the editor anytime after you initialized it. From an api for example:

```razor
@using Beamlak.Blazor.Utilities.Components
@using static Beamlak.Blazor.Utilities.Enums

<div style="width: 75%">
    <RichTextEditor @ref="textEditor">
        <EditorContent>
            <h1>Hello Rich Text World!</h1>
        </EditorContent>
    </RichTextEditor>
</div>

<button @onclick="loadContentFromApi">Load From Api</button>

@code {
    RichTextEditor textEditor;

    async Task loadContentFromApi()
    {
        // get your content from api here
        var content = await myApi.GetContent("url");
        await textEditor.LoadContent(content);
    }
}
```
<br>

### 2. BrowserStorage

Is a little utility class to help simplify working with browser storage in blazor applications. It can be used to store and fetch data from both `session storage` and `local storage`.

#### How to Use

If you haven't already installed it, install the nuget package using the package manager:
> Install-Package beamlak.blazor.utilities

Or you can install using visual studio package manager.

- Add the `IBrowserStorage` service as a dependency in your **Program.cs** file.

```cs
using Beamlak.Blazor.Utilities

builder.Services.AddScoped<IBrowserStorage, BrowserStorage>();
```

- inject the service to your pages/components:

```razor
@using Beamlak.Blazor.Utilities

@inject IBrowserStorage BrowserStorage
```
or

```cs
using Beamlak.Blazor.Utilities

[Inject] public IBrowserStorage BrowserStorage { get; set; }    // if you're using split component approach
```

- Store/Fetch data:

```cs
    async Task saveToSessionStorage()
    {
        var userData = new UserData { UserName = "John Doe", Age = 26 };
        await BrowserStorage.SaveItemAsync("UserData", userData, BrowserStorageType.SessionStorage);
    }

    async Task fetchFromSessionStorage()
    {
        var userData = await BrowserStorage.GetItemAsync<UserData>("UserData", BrowserStorageType.SessionStorage);
    }

    async Task saveToLocalStorage()
    {
        await BrowserStorage.SaveStringAsync("UserId", "user_id", BrowserStorageType.LocalStorage);
    }

    async Task fetchFromLocalStorage()
    {
        var userId = await BrowserStorage.GetStringAsync("UserId", BrowserStorageType.LocalStorage);
    }
```

- You can use custom keys for storing data:

```cs
    enum MyKeys
    {
        Username,
        SessionId,
        UserData
    }
    
    async Task saveToSessionStorage()
    {
        var userData = new UserData { UserName = "John Doe", Age = 26 };
        await BrowserStorage.SaveItemAsync(MyKeys.UserData, userData, BrowserStorageType.SessionStorage);
    }

    async Task fetchFromSessionStorage()
    {
        var userData = await BrowserStorage.GetItemAsync<UserData>(MyKeys.UserData, BrowserStorageType.SessionStorage);
    }

    async Task saveToLocalStorage()
    {
        await BrowserStorage.SaveStringAsync(MyKeys.Username, "user_name", BrowserStorageType.LocalStorage);
    }

    async Task fetchFromLocalStorage()
    {
        var username = await BrowserStorage.GetStringAsync(MyKeys.Username, BrowserStorageType.LocalStorage);
    }
```

### Helpful Articles

- [https://quilljs.com/guides/why-quill/](https://quilljs.com/guides/why-quill/)
- [https://www.puresourcecode.com/dotnet/blazor/create-a-blazor-component-for-quill/](https://www.puresourcecode.com/dotnet/blazor/create-a-blazor-component-for-quill/)
- [https://developer.mozilla.org/en-US/docs/Web/API/Window/localStorage](https://developer.mozilla.org/en-US/docs/Web/API/Window/localStorage)
- [https://www.w3schools.com/html/html5_webstorage.asp](https://www.w3schools.com/html/html5_webstorage.asp)
