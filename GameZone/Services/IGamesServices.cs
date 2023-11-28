﻿namespace GameZone.Services
{
    public interface IGamesServices
    {
        IEnumerable<Game> GetAll();
        Game? GetById(int id);

        Task Create(CreateGameFromViewModel model);
    }
}
