using RealEstate_Dapper_Api.Dtos.CategoryDtos;
using RealEstate_Dapper_Api.Models.DapperContext;
using Dapper;

namespace RealEstate_Dapper_Api.Repositories.CategoryRepository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly Context _context;

        public CategoryRepository(Context context)
        {
            _context = context;
        }

        public async Task CreateCategory(CreateCategoryDto categoryDto)
        {
            string query = "insert into Category (CategoryName, CategoryStatus) values (@categoryName,@categoryStatus)";
            var parametters = new DynamicParameters();
            parametters.Add("@categoryName", categoryDto.CategoryName);
            parametters.Add("@categoryStatus", true);
            using(var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parametters);
            }
        }

        public async Task DeleteCategory(int id)
        {
            string query = "Delete From Category Where CategoryID=@categoryID";
            var parametters = new DynamicParameters();
            parametters.Add("@categoryID", id);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parametters);
            }
        }

        public async Task<List<ResultCategoryDto>> GetAllCategoryAsync()
        {
            string query = "Select * From Category ";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultCategoryDto>(query);
                return values.ToList();
            }
        }

        public async Task<GetByIDCategoryDto> GetCategory(int id)
        {
            string query = "Select * From Category Where CategoryID=@categoryID";
            var parametters = new DynamicParameters();
            parametters.Add("@CategoryID", id);
            using(var connections = _context.CreateConnection())
            {
               var values= await connections.QueryFirstOrDefaultAsync<GetByIDCategoryDto>(query, parametters);
                return values;
            }
        }

        public async Task UpdateCategory(UpdateCategoryDto categoryDto)
        {
            string query = "Update Category Set CategoryName= @categoryName,CategoryStatus= @categoryStatus where CategoryID= @categoryID";
            var parametters = new DynamicParameters();
            parametters.Add("@categoryName" , categoryDto.CategoryName);
            parametters.Add("@categoryStatus", categoryDto.CategoryStatus);
            parametters.Add("@categoryID", categoryDto.CategoryID);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parametters);
            }
        }
    }
}
