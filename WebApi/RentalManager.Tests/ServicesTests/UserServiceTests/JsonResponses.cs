using System.Diagnostics.CodeAnalysis;

namespace RentalManager.Tests.ServicesTests.UserServiceTests;

[ExcludeFromCodeCoverage]
public static class JsonResponses
{
    public static string GetUserByIdResponse()
    {
        return """
               {
                 "pk": 1,
                 "username": "JohnKowalski",
                 "name": "JohnKowalski",
                 "is_active": true,
                 "last_login": "2024-01-01T00:00:00.000000Z",
                 "is_superuser": false,
                 "groups": [
                   "00000000-0000-0000-0000-000000000001",
                   "00000000-0000-0000-0000-000000000002"
                 ],
                 "groups_obj": [
                   {
                     "pk": "00000000-0000-0000-0000-000000000001",
                     "num_pk": 1,
                     "name": "House M.D. Fans",
                     "is_superuser": false,
                     "parent": null,
                     "parent_name": null,
                     "attributes": {}
                   },
                   {
                     "pk": "00000000-0000-0000-0000-000000000002",
                     "num_pk": 2,
                     "name": "Cuddy Fans",
                     "is_superuser": false,
                     "parent": "00000000-0000-0000-0000-000000000001",
                     "parent_name": "House M.D. Fans",
                     "attributes": {}
                   }
                 ],
                 "email": "",
                 "avatar": "",
                 "attributes": {
                   "last_name": "Kowalski",
                   "first_name": "John"
                 },
                 "uid": "0000000000000000000000000000000000000000000000000000000000000000",
                 "path": "users",
                 "type": "internal",
                 "uuid": "00000000-0000-0000-0000-000000000000"
               }
               """;
    }
    
    public static string GetUserByIdResponse_NotInternal()
    {
        return """
               {
                 "pk": 1,
                 "username": "JohnKowalski",
                 "name": "JohnKowalski",
                 "is_active": true,
                 "last_login": "2024-01-01T00:00:00.000000Z",
                 "is_superuser": false,
                 "groups": [
                   "00000000-0000-0000-0000-000000000001",
                   "00000000-0000-0000-0000-000000000002"
                 ],
                 "groups_obj": [
                   {
                     "pk": "00000000-0000-0000-0000-000000000001",
                     "num_pk": 1,
                     "name": "House M.D. Fans",
                     "is_superuser": false,
                     "parent": null,
                     "parent_name": null,
                     "attributes": {}
                   },
                   {
                     "pk": "00000000-0000-0000-0000-000000000002",
                     "num_pk": 2,
                     "name": "Cuddy Fans",
                     "is_superuser": false,
                     "parent": "00000000-0000-0000-0000-000000000001",
                     "parent_name": "House M.D. Fans",
                     "attributes": {}
                   }
                 ],
                 "email": "",
                 "avatar": "",
                 "attributes": {
                   "last_name": "Kowalski",
                   "first_name": "John"
                 },
                 "uid": "0000000000000000000000000000000000000000000000000000000000000000",
                 "path": "users",
                 "type": "external",
                 "uuid": "00000000-0000-0000-0000-000000000000"
               }
               """;
    }
    
    public static string GetUsersResponse()
    {
        return """
               {
                 "pagination": {
                   "next": 0,
                   "previous": 0,
                   "count": 5,
                   "current": 1,
                   "total_pages": 1,
                   "start_index": 1,
                   "end_index": 5
                 },
                 "results": [
                   {
                     "pk": 1,
                     "username": "JohnKowalski",
                     "name": "JohnKowalski",
                     "is_active": true,
                     "last_login": "2024-01-01T00:00:00.000000Z",
                     "is_superuser": false,
                     "groups": [
                       "00000000-0000-0000-0000-000000000001",
                       "00000000-0000-0000-0000-000000000002"
                     ],
                     "groups_obj": [
                       {
                         "pk": "00000000-0000-0000-0000-000000000001",
                         "num_pk": 1,
                         "name": "House M.D. Fans",
                         "is_superuser": false,
                         "parent": null,
                         "parent_name": null,
                         "attributes": {}
                       },
                       {
                         "pk": "00000000-0000-0000-0000-000000000002",
                         "num_pk": 2,
                         "name": "Cuddy Fans",
                         "is_superuser": false,
                         "parent": "00000000-0000-0000-0000-000000000001",
                         "parent_name": "House M.D. Fans",
                         "attributes": {}
                       }
                     ],
                     "email": "",
                     "avatar": "",
                     "attributes": {
                       "last_name": "Kowalski",
                       "first_name": "John"
                     },
                     "uid": "0000000000000000000000000000000000000000000000000000000000000000",
                     "path": "users",
                     "type": "internal",
                     "uuid": "00000000-0000-0000-0000-000000000000"
                   },
                   {
                     "pk": 2,
                     "username": "admin",
                     "name": "admin",
                     "is_active": true,
                     "last_login": "2024-01-01T00:00:00.000000Z",
                     "is_superuser": true,
                     "groups": [
                       "00000000-0000-0000-0000-000000000003"
                     ],
                     "groups_obj": [
                       {
                         "pk": "00000000-0000-0000-0000-000000000003",
                         "num_pk": 33698,
                         "name": "admins",
                         "is_superuser": true,
                         "parent": null,
                         "parent_name": null,
                         "attributes": {}
                       }
                     ],
                     "email": "",
                     "avatar": "",
                     "attributes": {
                       "last_name": "McAdmin",
                       "first_name": "Admin"
                     },
                     "uid": "0000000000000000000000000000000000000000000000000000000000000000",
                     "path": "users",
                     "type": "internal",
                     "uuid": "00000000-0000-0000-0000-000000000000"
                   },
                   {
                     "pk": 3,
                     "username": "service-account",
                     "name": "Service Account",
                     "is_active": true,
                     "last_login": null,
                     "is_superuser": false,
                     "groups": [],
                     "groups_obj": [],
                     "email": "",
                     "avatar": "",
                     "attributes": {},
                     "uid": "0000000000000000000000000000000000000000000000000000000000000000",
                     "path": "service-accounts",
                     "type": "service_account",
                     "uuid": "00000000-0000-0000-0000-000000000000"
                   }
                 ]
               }
               """;
    }
    
    public static string GetUsersResponse_MultipleUsers()
    {
        return """
               {
                 "pagination": {
                   "next": 0,
                   "previous": 0,
                   "count": 5,
                   "current": 1,
                   "total_pages": 1,
                   "start_index": 1,
                   "end_index": 5
                 },
                 "results": [
                   {
                     "pk": 1,
                     "username": "JohnKowalski",
                     "name": "JohnKowalski",
                     "is_active": true,
                     "last_login": "2024-01-01T00:00:00.000000Z",
                     "is_superuser": false,
                     "groups": [
                       "00000000-0000-0000-0000-000000000001",
                       "00000000-0000-0000-0000-000000000002"
                     ],
                     "groups_obj": [
                       {
                         "pk": "00000000-0000-0000-0000-000000000001",
                         "num_pk": 1,
                         "name": "House M.D. Fans",
                         "is_superuser": false,
                         "parent": null,
                         "parent_name": null,
                         "attributes": {}
                       },
                       {
                         "pk": "00000000-0000-0000-0000-000000000002",
                         "num_pk": 2,
                         "name": "Cuddy Fans",
                         "is_superuser": false,
                         "parent": "00000000-0000-0000-0000-000000000001",
                         "parent_name": "House M.D. Fans",
                         "attributes": {}
                       }
                     ],
                     "email": "",
                     "avatar": "",
                     "attributes": {
                       "last_name": "Kowalski",
                       "first_name": "John"
                     },
                     "uid": "0000000000000000000000000000000000000000000000000000000000000000",
                     "path": "users",
                     "type": "internal",
                     "uuid": "00000000-0000-0000-0000-000000000000"
                   },
                   {
                     "pk": 2,
                     "username": "admin",
                     "name": "admin",
                     "is_active": true,
                     "last_login": "2024-01-01T00:00:00.000000Z",
                     "is_superuser": true,
                     "groups": [
                       "00000000-0000-0000-0000-000000000003"
                     ],
                     "groups_obj": [
                       {
                         "pk": "00000000-0000-0000-0000-000000000003",
                         "num_pk": 33698,
                         "name": "admins",
                         "is_superuser": true,
                         "parent": null,
                         "parent_name": null,
                         "attributes": {}
                       }
                     ],
                     "email": "",
                     "avatar": "",
                     "attributes": {
                       "last_name": "McAdmin",
                       "first_name": "Admin"
                     },
                     "uid": "0000000000000000000000000000000000000000000000000000000000000000",
                     "path": "users",
                     "type": "internal",
                     "uuid": "00000000-0000-0000-0000-000000000000"
                   },
                   {
                     "pk": 3,
                     "username": "service-account",
                     "name": "Service Account",
                     "is_active": true,
                     "last_login": null,
                     "is_superuser": false,
                     "groups": [],
                     "groups_obj": [],
                     "email": "",
                     "avatar": "",
                     "attributes": {},
                     "uid": "0000000000000000000000000000000000000000000000000000000000000000",
                     "path": "service-accounts",
                     "type": "service_account",
                     "uuid": "00000000-0000-0000-0000-000000000000"
                   },
                   {
                     "pk": 4,
                     "username": "JaneDoe",
                     "name": "JaneDoe",
                     "is_active": true,
                     "last_login": "2024-01-01T00:00:00.000000Z",
                     "is_superuser": false,
                     "groups": [
                       "00000000-0000-0000-0000-000000000001"
                     ],
                     "groups_obj": [
                       {
                         "pk": "00000000-0000-0000-0000-000000000001",
                         "num_pk": 1,
                         "name": "House M.D. Fans",
                         "is_superuser": false,
                         "parent": null,
                         "parent_name": null,
                         "attributes": {}
                       }
                     ],
                     "email": "",
                     "avatar": "",
                     "attributes": {
                       "last_name": "Doe",
                       "first_name": "Jane"
                     },
                     "uid": "0000000000000000000000000000000000000000000000000000000000000000",
                     "path": "users",
                     "type": "internal",
                     "uuid": "00000000-0000-0000-0000-000000000000"
                   }
                 ]
               }
               """;
    }
}