db = db.getSiblingDB('gift-recommender')


db.createUser({
    user: 'admin',
    pwd: 'POFPASDKNFAKkuiw539847bskdjf__SDfsdf',
    roles: [
      {
        role: 'dbOwner',
      db: 'gift-recommender',
    },
  ],
});