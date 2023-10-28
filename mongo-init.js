// Connect to the admin database
db = db.getSiblingDB("admin");

// Create a user with admin privileges for the specified database
db.createUser({
  user: "root",
  pwd: "asdasd123",
  roles: [
    {
      role: "clusterAdmin",
      db: "admin"
    },
    {
      role: "dbOwner",
      db: "gift-recommender"
    },
    {
      role: "readWrite",
      db: "gift-recommender"
    }
  ]
});
