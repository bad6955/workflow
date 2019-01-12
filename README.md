# workflow

Linking Visual Studio with Github
        In Visual Studio on the right side, (same spot as Solution Explorer) select the middle tab 'Team Explorer'
        In the Team Explorer, go to the Connect menu by clicking the Green Plug icon next to the Home button at the top
        Under 'Local Git Repositories' select clone and enter the URL 'https://github.com/bad6955/workflow.git' and a path to where you                 want to store the files on your computer
        Create the clone of the repository
        Right click on the newly created repo in the 'Local Git Repositories' section and select 'Open in File Explorer'
        Navigate into the folder called 'Workflow' and open the Workflow.sln file to open the project in Visual Studio

With the 'Workflow.sln' file opened, in Visual Studio on the right side of the screen is the Solution Explorer, which shows all of the files related to the current solution
The main files for our project are .aspx, .aspx.cs, and .cs files:
        .aspx files are the HTML files, accepting any HTML code, as well as special <ASP:*** runat="server"/> tags 
        .aspx.cs files are the code-behind for the related .aspx files (press the little arrow on the .aspx files to see these)
              These are regular C# class files, with special events tied to the <ASP:***/> tags on the .aspx pages
        .cs files are just regular C# classes, for Models, database connections, helper classes, etc etc
        
        Check out the Login.aspx, Login.aspx.cs, and Dashboard.aspx.cs files for a quick commented example of this
