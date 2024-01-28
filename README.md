## Viet's personal blog.

Run this command to create migration
dotnet ef migrations add addDescription --verbose --project BasementBlog.Database --startup-project BasementBlog

Run this to apply migration
dotnet ef database update --verbose --project BasementBlog.Database --startup-project BasementBlog
