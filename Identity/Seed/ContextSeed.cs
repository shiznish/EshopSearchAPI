﻿using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity.Seed;
public static class ContextSeed
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        CreateRoles(modelBuilder);

        CreateBasicUsers(modelBuilder);

        MapUserRole(modelBuilder);
    }

    private static void CreateRoles(ModelBuilder modelBuilder)
    {
        List<IdentityRole> roles = DefaultRoles.IdentityRoleList();
        modelBuilder.Entity<IdentityRole>().HasData(roles);
    }

    private static void CreateBasicUsers(ModelBuilder modelBuilder)
    {
        List<ApplicationUser> users = DefaultUser.IdentityBasicUserList();
        modelBuilder.Entity<ApplicationUser>().HasData(users);
    }

    private static void MapUserRole(ModelBuilder modelBuilder)
    {
        var identityUserRoles = MappingUserRole.IdentityUserRoleList();
        modelBuilder.Entity<IdentityUserRole<string>>().HasData(identityUserRoles);
    }
}