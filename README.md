![Chapter](https://raw.githubusercontent.com/dwndland/Chapter.Net.WPF.DragAndDrop/master/Icon.png)

# Chapter.Net.WPF.DragAndDrop Library

## Overview
Chapter.Net.WPF.DragAndDrop brings objects and helper to work with drag and drop within the WPF application.

## Features
- **DragItem:** Allow drag selected item(s) or data context from any control.
- **DropItem:** Allow drop of a particular type of files to a target controls.
- **DropItems:** Allow drop of one or more particular types of items to a target control.

## Getting Started

1. **Installation:**
    - Install the Chapter.Net.WPF.DragAndDrop library via NuGet Package Manager:
    ```bash
    dotnet add package Chapter.Net.WPF.DragAndDrop
    ```

2. **DropItem:**
    - Usage
    ```xaml
    <ListBox AllowDrop="True">
        <chapter:Drop.DropItem>
            <chapter:DropItem Drops="Files" Effect="Copy" Command="{Binding DropFilesCommand}" />
        </chapter:Drop.DropItem>
    </ListBox>
    ```
    ```csharp
    public void DemoViewModel : ObservableObject
    {
        public DemoViewModel()
        {
            DropFilesCommand = new DelegateCommand<ItemsDroppedArgs>(DropFiles);
        }

        public IDelegateCommand DropFilesCommand { get; }

        private void DropFiles(ItemsDroppedArgs e)
        {
            //e.Items[]
        }
    }
    ```

3. **DropItems:**
    - Usage
    ```xaml
    <ListBox AllowDrop="True">
        <chapter:Drop.DropItems>
            <chapter:DropItemCollection>
                <chapter:DropItem Drops="File" Effect="Copy" Command="{Binding DropFileCommand}" />
                <chapter:DropItem Drops="Folder" Effect="Copy" Command="{Binding DropFolderCommand}" />
            </chapter:DropItemCollection>
        </chapter:Drop.DropItems>
    </ListBox>
    ```
    ```csharp
    public void DemoViewModel : ObservableObject
    {
        public DemoViewModel()
        {
            DropFileCommand = new DelegateCommand<ItemDroppedArgs>(DropFile);
            DropFolderCommand = new DelegateCommand<ItemDroppedArgs>(DropFolder);
        }

        public IDelegateCommand DropFileCommand { get; }
        public IDelegateCommand DropFolderCommand { get; }

        private void DropFile(ItemDroppedArgs e)
        {
            //e.Item
        }

        private void DropFolder(ItemDroppedArgs e)
        {
            //e.Item
        }
    }
    ```

4. **DragItem:**
    - Usage
    ```xaml
    <ListBox>
        <chapter:Drag.DragItem>
            <chapter:DropItem Drags="SelectedItem" Effect="Copy" Format="SelectedItem" />
        </chapter:Drag.DragItem>
    </ListBox>
    ```
    ```xaml
    <ListBox AllowDrop="True">
        <chapter:Drop.DropItem>
            <chapter:DropItem Drops="Custom" Effect="Copy" Format="SelectedItem" Command="{Binding DropItemCommand}" />
        </chapter:Drop.DropItem>
    </ListBox>
    ```
    ```csharp
    public void DemoViewModel : ObservableObject
    {
        public DemoViewModel()
        {
            DropItemCommand = new DelegateCommand<CustomDroppedArgs>(DropItem);
        }

        public IDelegateCommand DropItemCommand { get; }

        private void DropItem(CustomDroppedArgs e)
        {
            //e.Item
        }
    }
    ```

## Links
* [NuGet](https://www.nuget.org/packages/Chapter.Net.WPF.DragAndDrop)
* [GitHub](https://github.com/dwndland/Chapter.Net.WPF.DragAndDrop)

## License
Copyright (c) David Wendland. All rights reserved.
Licensed under the MIT License. See LICENSE file in the project root for full license information.
