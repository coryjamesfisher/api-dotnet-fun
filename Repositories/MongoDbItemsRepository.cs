using System;
using System.Collections.Generic;
using System.Linq;
using dotnet_restapi.Entities;
using MongoDB.Driver;

namespace dotnet_restapi.Repositories 
{
    public class MongoDbItemsRepository : IItemsRepository
    {
        private const string databaseName = "catalog";
        private const string collectionName = "items";

        private readonly IMongoCollection<Item> itemsCollection;

        public MongoDbItemsRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            itemsCollection = database.GetCollection<Item>(collectionName);
        }

        public IEnumerable<Item> GetItems()
        {
            return itemsCollection.Find<Item>(FilterDefinition<Item>.Empty).ToList();
        }

        public Item GetItem(Guid id)
        {
            return itemsCollection.Find<Item>(new ExpressionFilterDefinition<Item>(item => item.Id == id))
                .FirstOrDefault();
        }

        public void CreateItem(Item item)
        {
            itemsCollection.InsertOne(item);
        }

        public void UpdateItem(Item item)
        {
            itemsCollection.ReplaceOne(new ExpressionFilterDefinition<Item>(existingItem => existingItem.Id == item.Id), item);
        }

        public void DeleteItem(Guid id)
        {
            itemsCollection.DeleteOne(new ExpressionFilterDefinition<Item>(item => item.Id == id));
        }
    }
}