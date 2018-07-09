# MyMediaLocator (formerly CCrawler)
## IT 4400, University of Missouri, Fall 2017

This is a WPF utility used to list out files of comparable type after a recurisve directory search.

TagLib-Sharp 2.1 was used for this project, available via NuGet Package Manager.

Changes from project proposal:
    - Threading was not used, instead I researched how to use lazy evaluation and Directory.EnumerateFiles() to have nearly-matching performance for a lot less work
    - Save format changed from JSON to CSV, because it works better with more applications, and allows the user to open their listing of located media inside Microsoft Excel
    - Packaged relevant file extensions together instead of making the user search for one extension at a time

Project Hierarchy:
    - MainWindow.xaml
        - MainWindow.xaml.cs
    - Filetype.cs
        - Audio.cs
        - Photo.cs
        - Video.cs
    - SaveHandler.cs