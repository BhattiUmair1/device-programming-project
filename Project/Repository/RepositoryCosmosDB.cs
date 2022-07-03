using Microsoft.Azure.Cosmos;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository
{
    public static class RepositoryCosmosDB
    {
        public static async void SendFavoriteFlight(DepartureData SelectedObject)
        {
            try
            {
                SelectedObject.ImageLike = null;
                SelectedObject.DImageFlightDepature = null;
                SelectedObject.Trashcan = null;

                string connectionString = "AccountEndpoint=https://xamarinprojectcosmos.documents.azure.com:443/;AccountKey=NzEDjPNVqMd6MiaEta6l6k56gctzk29nw4yPuDFeKJRrnGKzmHkC38Yny1R0GICarn5iRyybh6xl5KpzKeQelQ==;";
                CosmosClient cosmosClient = new CosmosClient(connectionString);
                Database database = cosmosClient.GetDatabase("FlightCosmosDB");
                Container container = database.GetContainer("Flights");
                await container.CreateItemAsync(SelectedObject, new PartitionKey(SelectedObject.Email));
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public static async Task<List<DepartureData>> GetFavoriteFlights(string Email)
        {
            try
            {
                Debug.WriteLine("Requested");
                string connectionString = "AccountEndpoint=https://xamarinprojectcosmos.documents.azure.com:443/;AccountKey=NzEDjPNVqMd6MiaEta6l6k56gctzk29nw4yPuDFeKJRrnGKzmHkC38Yny1R0GICarn5iRyybh6xl5KpzKeQelQ==;";
                CosmosClient cosmosClient = new CosmosClient(connectionString);
                Database database = cosmosClient.GetDatabase("FlightCosmosDB");
                Container container = database.GetContainer("Flights");

                List<DepartureData> FavoriteFlights = new List<DepartureData>();

                QueryDefinition query = new QueryDefinition("Select * From Flights f Where f.Email = @Email");
                query.WithParameter("@Email", Email);

                FeedIterator<DepartureData> iterator = container.GetItemQueryIterator<DepartureData>(query);
                while (iterator.HasMoreResults)
                {
                    FeedResponse<DepartureData> response = await iterator.ReadNextAsync();
                    FavoriteFlights.AddRange(response);
                }

                return FavoriteFlights;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static async void DeleteFlight(DepartureData SelectedObject)
        {
            try
            {
                Debug.WriteLine("Requested");
                string connectionString = "AccountEndpoint=https://xamarinprojectcosmos.documents.azure.com:443/;AccountKey=NzEDjPNVqMd6MiaEta6l6k56gctzk29nw4yPuDFeKJRrnGKzmHkC38Yny1R0GICarn5iRyybh6xl5KpzKeQelQ==;";
                CosmosClient cosmosClient = new CosmosClient(connectionString);
                Database database = cosmosClient.GetDatabase("FlightCosmosDB");
                Container container = database.GetContainer("Flights");

                await container.DeleteItemAsync<DepartureData>(SelectedObject.Id, new PartitionKey(SelectedObject.Email));
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
