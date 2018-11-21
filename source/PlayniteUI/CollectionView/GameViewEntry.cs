﻿using Playnite.SDK.Models;
using Playnite.SDK.Plugins;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayniteUI
{
    public class GameViewEntry : INotifyPropertyChanged
    {
        private ILibraryPlugin plugin;
        private GamesCollectionView view;

        public Guid Id => Game.Id;
        public Guid PluginId => Game.PluginId;
        public string GameId => Game.GameId;
        public ComparableList<Category> Categories => Game.Categories;
        public Platform Platform => Game.Platform;
        public ComparableList<Tag> Tags => Game.Tags;
        public ComparableList<Genre> Genres => Game.Genres;
        public DateTime? ReleaseDate => Game.ReleaseDate;
        public int? ReleaseYear => Game.ReleaseYear;
        public DateTime? LastActivity => Game.LastActivity;
        public ComparableList<Company> Developers => Game.Developers;
        public ComparableList<Company> Publishers => Game.Publishers;
        public ObservableCollection<Link> Links => Game.Links;
        public string Icon => Game.Icon;
        public string CoverImage => Game.CoverImage;
        public string BackgroundImage => Game.BackgroundImage;
        public bool Hidden => Game.Hidden;
        public bool Favorite => Game.Favorite;
        public string InstallDirectory => Game.InstallDirectory;
        public Guid PlatformId => Game.PlatformId;
        public ObservableCollection<GameAction> OtherActions => Game.OtherActions;
        public GameAction PlayAction => Game.PlayAction;
        public string DisplayName => Game.Name;
        public string Description => Game.Description;
        public bool IsInstalled => Game.IsInstalled;
        public bool IsInstalling => Game.IsInstalling;
        public bool IsUnistalling => Game.IsUninstalling;
        public bool IsLaunching => Game.IsLaunching;
        public bool IsRunning => Game.IsRunning;
        public long Playtime => Game.Playtime;
        public DateTime? Added => Game.Added;
        public DateTime? Modified => Game.Modified;
        public long PlayCount => Game.PlayCount;
        public Series Series => Game.Series;
        public string Version => Game.Version;
        public AgeRating AgeRating => Game.AgeRating;
        public Region Region => Game.Region;
        public GameSource Source => Game.Source;
        public CompletionStatus CompletionStatus => Game.CompletionStatus;
        public int? UserScore => Game.UserScore;
        public int? CriticScore => Game.CriticScore;
        public int? CommunityScore => Game.CommunityScore;

        public ComparableList<Guid> CategoryIds => Game.CategoryIds;
        public ComparableList<Guid> GenreIds => Game.GenreIds;
        public ComparableList<Guid> DeveloperIds => Game.DeveloperIds;
        public ComparableList<Guid> PublisherIds => Game.PublisherIds;
        public ComparableList<Guid> TagIds => Game.TagIds;
        public Guid SeriesId => Game.SeriesId;
        public Guid AgeRatingId => Game.AgeRatingId;
        public Guid RegionId => Game.RegionId;
        public Guid SourceId => Game.SourceId;

        public object IconObject => GetImageObject(Game.Icon);
        public object CoverImageObject => GetImageObject(Game.CoverImage);
        public object BackgroundImageObject => GetImageObject(Game.BackgroundImage);
        public object DefaultIconObject => GetImageObject(DefaultIcon);
        public object DefaultCoverImageObject => GetImageObject(DefaultCoverImage);
        
        public string Name
        {
            get
            {
                return string.IsNullOrEmpty(Game.SortingName) ? Game.Name : Game.SortingName;
            }
        }

        public Game Game
        {
            get; set;
        }

        public string Provider
        {
            get
            {
                if (string.IsNullOrEmpty(plugin?.Name))
                {
                    return "Playnite";
                }
                else
                {
                    return plugin.Name;
                }
            }
        }

        public string DefaultIcon
        {
            get
            {                
                if (!string.IsNullOrEmpty(Platform?.Icon))
                {
                    return Platform.Icon;
                }
                else
                {
                    if (!string.IsNullOrEmpty(plugin?.LibraryIcon))
                    {
                        return plugin.LibraryIcon;
                    }

                    return @"resources:/Images/icon_dark.png";
                }
            }
        }

        public string DefaultCoverImage
        {
            get
            {
                if (!string.IsNullOrEmpty(Platform?.Cover))
                {
                    return Platform.Cover;
                }
                else
                {
                    return @"resources:/Images/custom_cover_background.png";
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public GameViewEntry(Game game, string category, GamesCollectionView view, ILibraryPlugin plugin)
        {
            this.plugin = plugin;
            this.view = view;
            Game = game;
            Game.PropertyChanged += Game_PropertyChanged;
        }

        private void Game_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            //if (propertyName == nameof(Game.PlatformId))
            //{
            //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Platform)));
            //}

            if (propertyName == nameof(Game.SortingName) || propertyName == nameof(Game.Name))
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Game.Name)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DisplayName)));
            }

            if (propertyName == nameof(Game.Icon))
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IconObject)));
            }

            if (propertyName == nameof(Game.CoverImage))
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CoverImageObject)));
            }

            if (propertyName == nameof(Game.BackgroundImage))
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BackgroundImageObject)));
            }
        }

        private object GetImageObject(string data)
        {
            return CustomImageStringToImageConverter.GetImageFromSource(data);
        }

        //public override string ToString()
        //{
        //    return string.Format("{0}, {1}", Name, Category);
        //}

        public static explicit operator Game(GameViewEntry entry)
        {
            return entry.Game;
        }
    }
}
