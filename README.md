# workflow

Linking Visual Studio with Github:
        1. In Visual Studio on the right side, (same spot as Solution Explorer) select the middle tab 'Team Explorer'
        2. In the Team Explorer, go to the Connect menu by clicking the Green Plug icon next to the Home button at the top
        3. Under 'Local Git Repositories' select clone and enter the URL 'https://github.com/bad6955/workflow.git' and a path to where you                 want to store the files on your computer
        4. Create the clone of the repository
        5. Right click on the newly created repo in the 'Local Git Repositories' section and select 'Open in File Explorer'
        6. Navigate into the folder called 'Workflow' and open the Workflow.sln file to open the project in Visual Studio

Navigating the Solution in Visual Studio:
        1. With the 'Workflow.sln' file opened, in Visual Studio on the right side of the screen is the Solution Explorer, which shows all of the files related to the current solution
        2. The main files for our project are .aspx, .aspx.cs, and .cs files:
               a) .aspx files are the HTML files, accepting any HTML code, as well as special <ASP:*** runat="server"/> tags 
               b) .aspx.cs files are the code-behind for the related .aspx files (press the little arrow on the .aspx files to see these)
                      These are regular C# class files, with special events tied to the <ASP:***/> tags on the .aspx pages
               c) .cs files are just regular C# classes, for Models, database connections, helper classes, etc etc
         3. Check out the Login.aspx, Login.aspx.cs, and Dashboard.aspx.cs files for a quick commented example of this
