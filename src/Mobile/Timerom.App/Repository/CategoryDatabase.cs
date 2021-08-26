using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Timerom.App.ValueObjects.Entity;
using Timerom.App.ValueObjects.Enuns;

namespace Timerom.App.Repository
{
    public class CategoryDatabase
    {
        private static SQLiteAsyncConnection _database;
        private static readonly AsyncLazy<CategoryDatabase> _instance = new AsyncLazy<CategoryDatabase>(async () =>
        {
            CategoryDatabase instance = new CategoryDatabase();
            CreateTableResult result = await _database.CreateTableAsync<Category>();
            if(result == CreateTableResult.Created)
                await Migrations();

            return instance;
        });

        public CategoryDatabase()
        {
            _database = new SQLiteAsyncConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "timerom.db3"));
        }

        public static async Task<CategoryDatabase> Instance()
        {
            return await _instance;
        }

        public async Task Save(Category category)
        {
            _ = await _database.InsertAsync(category);
        }

        public async Task Save(IList<Category> categoryList)
        {
            _ = await _database.InsertAllAsync(categoryList);
        }

        public async Task<List<Category>> GetAll()
        {
            return await _database.Table<Category>().ToListAsync();
        }

        public async Task<bool> ExistParentCategoryWithName(string name)
        {
            var count = await _database.Table<Category>().CountAsync(c => c.Name.ToUpper().Equals(name.ToUpper()) && c.ParentCategoryId == null);
            return count > 0;
        }

        public async Task<Category> GetById(long id)
        {
            return await _database.Table<Category>().FirstAsync(c => c.Id == id);
        }
        public async Task<List<Category>> GetChildrensByParentId(long id)
        {
            return await _database.Table<Category>().Where(c => c.ParentCategoryId == id).ToListAsync();
        }

        public async Task Update(Category category)
        {
            _ = await _database.UpdateAsync(category);
        }
        public async Task Delete(Category category)
        {
            _ = await _database.DeleteAsync(category);
        }

        private static async Task Migrations()
        {
            await _database.InsertAllAsync(FoodCategory());
            await _database.InsertAllAsync(WorkCategory());
            await _database.InsertAllAsync(SportCategory());
            await _database.InsertAllAsync(StudyCategory());
            await _database.InsertAllAsync(PurchasesCategory());
            await _database.InsertAllAsync(PersonalProjectsCategory());
            await _database.InsertAllAsync(FamilyCategory());
            await _database.InsertAllAsync(SocialMidiaCategory());
        }

        private static List<Category> FoodCategory()
        {
            return new List<Category>
            {
                new Category
                {
                    Id = 1,
                    Name = ResourceText.TITLE_FOOD,
                    Type = CategoryType.Neutral
                },
                new Category
                {
                    Id = 2,
                    Name = ResourceText.TITLE_LUNCH,
                    Type = CategoryType.Neutral,
                    ParentCategoryId = 1
                },
                new Category
                {
                    Id = 3,
                    Name = ResourceText.TITLE_DINNER,
                    Type = CategoryType.Neutral,
                    ParentCategoryId = 1
                },
                new Category
                {
                    Id = 4,
                    Name = ResourceText.TITLE_BREAKFAST,
                    Type = CategoryType.Neutral,
                    ParentCategoryId = 1
                }
            };
        }
        private static List<Category> WorkCategory()
        {
            return new List<Category>
            {
                new Category
                {
                    Id = 5,
                    Name = ResourceText.TITLE_WORK,
                    Type = CategoryType.Productive
                },
                new Category
                {
                    Id = 6,
                    Name = ResourceText.TITLE_MEETINGS,
                    Type = CategoryType.Productive,
                    ParentCategoryId = 5
                },
                new Category
                {
                    Id = 7,
                    Name = ResourceText.TITLE_TASKS,
                    Type = CategoryType.Productive,
                    ParentCategoryId = 5
                }
            };
        }
        private static List<Category> SportCategory()
        {
            return new List<Category>
            {
                new Category
                {
                    Id = 8,
                    Name = ResourceText.TITLE_SPORT,
                    Type = CategoryType.Productive
                },
                new Category
                {
                    Id = 9,
                    Name = ResourceText.TITLE_WALKING,
                    Type = CategoryType.Productive,
                    ParentCategoryId = 8
                },
                new Category
                {
                    Id = 10,
                    Name = ResourceText.TITLE_GYM,
                    Type = CategoryType.Productive,
                    ParentCategoryId = 8
                }
            };
        }
        private static List<Category> StudyCategory()
        {
            return new List<Category>
            {
                new Category
                {
                    Id = 11,
                    Name = ResourceText.TITLE_STUDIES,
                    Type = CategoryType.Productive
                },
                new Category
                {
                    Id = 12,
                    Name = ResourceText.TITLE_COURSES,
                    Type = CategoryType.Productive,
                    ParentCategoryId = 11
                },
                new Category
                {
                    Id = 13,
                    Name = ResourceText.TITLE_BOOKS,
                    Type = CategoryType.Productive,
                    ParentCategoryId = 11
                },
                new Category
                {
                    Id = 14,
                    Name = ResourceText.TITLE_PODCASTS,
                    Type = CategoryType.Productive,
                    ParentCategoryId = 11
                }
            };
        }
        private static List<Category> PurchasesCategory()
        {
            return new List<Category>
            {
                new Category
                {
                    Id = 15,
                    Name = ResourceText.TITLE_PURCHASES,
                    Type = CategoryType.Neutral
                },
                new Category
                {
                    Id = 16,
                    Name = ResourceText.TITLE_SUPERMARKET,
                    Type = CategoryType.Neutral,
                    ParentCategoryId = 15
                },
                new Category
                {
                    Id = 17,
                    Name = ResourceText.TITLE_TECHNOLOGY,
                    Type = CategoryType.Neutral,
                    ParentCategoryId = 15
                },
                new Category
                {
                    Id = 18,
                    Name = ResourceText.TITLE_PRODUCT_SEARCH,
                    Type = CategoryType.Neutral,
                    ParentCategoryId = 15
                }
            };
        }
        private static List<Category> PersonalProjectsCategory()
        {
            return new List<Category>
            {
                new Category
                {
                    Id = 19,
                    Name = ResourceText.TITLE_PERSONAL_PROJECTS,
                    Type = CategoryType.Productive
                },
                new Category
                {
                    Id = 20,
                    Name = ResourceText.TITLE_TASKS,
                    Type = CategoryType.Productive,
                    ParentCategoryId = 19
                }
            };
        }
        private static List<Category> FamilyCategory()
        {
            return new List<Category>
            {
                new Category
                {
                    Id = 21,
                    Name = ResourceText.TITLE_FAMILY,
                    Type = CategoryType.Neutral
                },
                new Category
                {
                    Id = 22,
                    Name = ResourceText.TITLE_TOUR,
                    Type = CategoryType.Neutral,
                    ParentCategoryId = 21
                },
                new Category
                {
                    Id = 23,
                    Name = ResourceText.TITLE_CALL,
                    Type = CategoryType.Neutral,
                    ParentCategoryId = 21
                }
            };
        }
        private static List<Category> SocialMidiaCategory()
        {
            return new List<Category>
            {
                new Category
                {
                    Id = 24,
                    Name = ResourceText.TITLE_SOCIAL_MEDIA,
                    Type = CategoryType.Unproductive
                },
                new Category
                {
                    Id = 25,
                    Name = ResourceText.TITLE_INSTAGRAM,
                    Type = CategoryType.Unproductive,
                    ParentCategoryId = 24
                },
                new Category
                {
                    Id = 26,
                    Name = ResourceText.TITLE_FACEBOOK,
                    Type = CategoryType.Unproductive,
                    ParentCategoryId = 24
                },
                new Category
                {
                    Id = 27,
                    Name = ResourceText.TITLE_YOUTUBE,
                    Type = CategoryType.Unproductive,
                    ParentCategoryId = 24
                }
            };
        }
    }
}