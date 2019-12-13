

# Sample Program

The purpose of this sample is to understand your capabilities, speed, understanding of the technologies and your internal development style.

The following tools are necessary for this sample:

 - Visual Studio 
 - SQL Server

The following technologies need to be applied on the sample project

 - DropZoneJS - [https://www.dropzonejs.com/](https://www.dropzonejs.com/)
 - DataTablesJS - [https://www.datatables.net/](https://www.datatables.net/)
 - jQuery
 - JavaScript
 - MVC Core
 - Entity Framework Core
 - Web API
 - Identity Server

High Level Steps:

 - Create a .Net Core web project MVC with individual user accounts
 - Convert/Move/Export localdb database to your MSSQLSERVER instance
 - Change your connection string to point to that instance
 - Add 15-20 users to the database
	 - Add a Web API project
	 - Create a method to authenticate
	 - Create a method to authorize
	 - Create methods to work with Identity Server (CRUD)
 - Create a method that will call the WebAPI to authenticate and
   authorize the user
 - Initialize the headers with the authorization code
 - From the home page in MVC use jQuery to call the controller for a
   list of users
 - Create a method that will give a list of users from Identity Server
 - The controller will return json
 - The json results will be displayed on the DataTable (simple
   capabilities: search, paging and ability to create, update, delete
   (soft delete))
 - Create a table in SQL Server that stores the documents (use a true
   file system not database to store the document)
 - Create methods for:
	 - Upon selection of any record allow the user to upload any document(s) using DropZoneJS and displays any record of any documents stored.
		 - Ability to click the document to download
	 - Allow drag and drop (Chrome)
	 - Upon upload persist the document data
	 - Allow to delete the document (soft delete)
