using System.ComponentModel.DataAnnotations;

namespace Wieczorna_nauka_aplikacja_webowa.Models
{
    public class CreateRentalCarDto
    {
        public long Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        public string Description { get; set; }
        public bool Abroad { get; set; }
        public bool Advance { get; set; }
        public long UserId { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostCode { get; set; }
    }
}

/*{
 "Email": "RentalCar-Sebcio",
 "Password": "sebcio wypozycza",
 "Abroad": false,
 "Advance": true,
 "City": "Poznan",
 "Street": "Długa",
 "PostCode": "05-090"
}

eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjUiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiICIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IlVzZXIiLCJEYXRlT2ZCaXJ0aCI6IjE5OTItMDEtMDEiLCJOYXRpb25hbGl0eSI6IlBvbGFuZCIsImV4cCI6MTY3NDE1OTQzNCwiaXNzIjoiaHR0cDovL3JlbnRhbGNhcmFwaS5jb20iLCJhdWQiOiJodHRwOi8vcmVudGFsY2FyYXBpLmNvbSJ9.3TbuZyfB0BXf8FuK4YSqrKVUIUIiym8FP0yoRJLGIek

eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjYiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiICIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6Ik1hbmFnZXIiLCJEYXRlT2ZCaXJ0aCI6IjE5OTItMDEtMDEiLCJOYXRpb25hbGl0eSI6IlBvbGFuZCIsImV4cCI6MTY3NDE2MDM3OCwiaXNzIjoiaHR0cDovL3JlbnRhbGNhcmFwaS5jb20iLCJhdWQiOiJodHRwOi8vcmVudGFsY2FyYXBpLmNvbSJ9.57rwy1LWZoJaQTNLBxeKKeCDEQXMnYzWiYgv48W-oT4
 "Email": "sebcio1@wp.pl",
 "Password": "sebcio1",
 "ConfirmPassword": "sebcio1",
 "Nationality": "Poland",
 "DateOfBirth": "1992-01-01",
 "RoleId": 2


 "Email": "sebcio@wp.pl",
 "Password": "sebcio",

{
 "Email": "sebcioadmin@wp.pl",
 "Password": "sebcioadmin"
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjciLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiICIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkFkbWluIiwiRGF0ZU9mQmlydGgiOiIxOTk1LTAxLTAxIiwiTmF0aW9uYWxpdHkiOiJHZXJtYW55IiwiZXhwIjoxNjc0MTYwNzI1LCJpc3MiOiJodHRwOi8vcmVudGFsY2FyYXBpLmNvbSIsImF1ZCI6Imh0dHA6Ly9yZW50YWxjYXJhcGkuY29tIn0.Gdyjl64TFVrz8xswkJVUwi5dX9jHuOlifzHQndYCgJQ
}

eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjciLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiICIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkFkbWluIiwiRGF0ZU9mQmlydGgiOiIxOTk1LTAxLTAxIiwiTmF0aW9uYWxpdHkiOiJHZXJtYW55IiwiZXhwIjoxNjc0MTYxODM3LCJpc3MiOiJodHRwOi8vcmVudGFsY2FyYXBpLmNvbSIsImF1ZCI6Imh0dHA6Ly9yZW50YWxjYXJhcGkuY29tIn0.Fn-ybuYHFnQdbQ97uCTtIQAEaw6MhS2kFV9eEJyGMUg

 "Email": "admin1@wp.pl",
 "Password": "admin1",
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjExIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6IiAiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJNYW5hZ2VyIiwiRGF0ZU9mQmlydGgiOiIxOTkyLTAxLTAxIiwiZXhwIjoxNjc0MjQ3MDI3LCJpc3MiOiJodHRwOi8vcmVudGFsY2FyYXBpLmNvbSIsImF1ZCI6Imh0dHA6Ly9yZW50YWxjYXJhcGkuY29tIn0.1IVYBKeakEEWGlMKUPjmGlvRFdKElB5JhFpMqZAeMCk

*/