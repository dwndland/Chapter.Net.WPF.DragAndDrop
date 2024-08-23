<img src="https://raw.githubusercontent.com/dwndland/Chapter.Net.WPF.DragAndDrop/master/Icon.png" alt="logo" width="64"/>

# Chapter.Net.WPF.DragAndDrop Library

## Overview
Chapter.Net.WPF.DragAndDrop brings objects and helper to work with drag and drop within the WPF application.

## Features
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
            <chapter:DropItem DropType="Files" DragDropEffect="Copy" Command="{Binding DropFilesCommand}" />
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
                <chapter:DropItem DropType="File" DragDropEffect="Copy" Command="{Binding DropFileCommand}" />
                <chapter:DropItem DropType="Folder" DragDropEffect="Copy" Command="{Binding DropFolderCommand}" />
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

## Links
* [NuGet](https://www.nuget.org/packages/Chapter.Net.WPF.DragAndDrop)
* [GitHub](https://github.com/dwndland/Chapter.Net.WPF.DragAndDrop)

## License
Copyright (c) David Wendland. All rights reserved.
Licensed under the MIT License. See LICENSE file in the project root for full license information.
