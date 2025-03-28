﻿using PET.Interfaces;
using PET.Models;
using System.Text.Json;

namespace PET.Services
{
    public class TagService : ITag
    {
        // path to tag json file
        private readonly string tagsFilePath = Path.Combine(AppContext.BaseDirectory, "Details", "Tags.json");

        // Method to save a new tag
        public async Task AddTagAsync(Tags tag)
        {
            try
            {
                var tags = await LoadAllTagsAsync();
                tags.Add(tag);
                await SaveAllTagAsync(tags);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving tag: {ex.Message}");
                throw;
            }
        }

        // Method to load tags from the json file
        public async Task<List<Tags>> LoadAllTagsAsync()
        {
            try
            {
                if (!File.Exists(tagsFilePath))
                {
                    return new List<Tags>();
                }

                var json = await File.ReadAllTextAsync(tagsFilePath);
                //convert the json string into a list of tags
                return JsonSerializer.Deserialize<List<Tags>>(json) ?? new List<Tags>();
            }
            catch (JsonException jsonEx)
            {
                Console.WriteLine($"JSON deserialization error: {jsonEx.Message}");
                return new List<Tags>();

            }
            catch (IOException ioEx)
            {
                Console.WriteLine($"I/O error while loading tags: {ioEx.Message}");
                return new List<Tags>();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error while loading tags: {ex.Message}");
                return new List<Tags>();
            }
        }

        // Method to save the entire tag list to the json file
        public async Task SaveAllTagAsync(List<Tags> tags)
        {
            try
            {
                // Convert the tag list to a JSON string with indentation
                var json = JsonSerializer.Serialize(tags, new JsonSerializerOptions { WriteIndented = true });
                await File.WriteAllTextAsync(tagsFilePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error while saving tags: {ex.Message}");
                throw;
            }
        }
    }
}
