using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedUsers(DataContext context)
        {
            if (await context.Accounts.AnyAsync()) return;

            var accountData = await File.ReadAllTextAsync("Data/AccountSeed.json");

            var options = new JsonSerializerOptions{PropertyNameCaseInsensitive = true};

            var accounts = JsonSerializer.Deserialize<List<Account>>(accountData);

            foreach (var acc in accounts)
            {
                using var hmac = new HMACSHA512();
                acc.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("asdfghjkl"));
                acc.PasswordSalt = hmac.Key;

                context.Add(acc);
            }

            await context.SaveChangesAsync();
        }
        public static async Task SeedRace(DataContext context)
        {
            if (await context.Races.AnyAsync()) return;
            Race race = new Race
            {
                RaceName = "Seed",
                RaceAlbum = new Album {
                    AlbumName = "Seed"
                }
            };
            context.Races.Add(race);
            await context.SaveChangesAsync();
            int pos = 1;
            foreach (Account acc in context.Accounts.AsEnumerable())
            {
                race.RaceAttendee.Add(
                    new RaceAttendance
                    {
                        RaceId= race.RaceId,
                        BibNumber = pos.ToString(),
                        Runner = acc,
                        Position = pos++,
                        Duration = new TimeSpan(1,23 + pos, pos),
                        FinishTime = DateTime.UtcNow + new TimeSpan(1,23 + pos, pos)
                    }
                );
                context.Races.Update(race);
            }
            await context.SaveChangesAsync();

        }
    }
}