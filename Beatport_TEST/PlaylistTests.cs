using Beatport_BLL;
using Beatport_BLL.Exceptions;
using Beatport_BLL.Interfaces;
using Beatport_BLL.Models.Dtos;
using Moq;

namespace Beatport_TEST;

public class PlaylistTests
{
   private Mock<IPlaylistRepository> _mockPlaylistRepository;
   private PlaylistService _playlistService;

   [SetUp]
   public void Setup()
   {
       _mockPlaylistRepository = new Mock<IPlaylistRepository>();
       _playlistService = new PlaylistService(_mockPlaylistRepository.Object);
   }

   [Test]
   public void GetAllPlaylists_ShouldReturnListOfPlaylists()
   {
       // Arrange
       int userId = 1;
       List<PlaylistDto> expectedPlaylists = new List<PlaylistDto>
       {
           new() { Id = 1, Title = "Test Playlist 1" },
           new() { Id = 2, Title = "Test Playlist 2" }
       };
       _mockPlaylistRepository.Setup(x => x.GetAllPlaylists(userId)).Returns(expectedPlaylists);

       // Act
       List<PlaylistDto> result = _playlistService.GetAllPlaylists(userId);

       // Assert
       Assert.That(result, Is.EqualTo(expectedPlaylists));
   }

   [Test]
   public void GetAllPlaylists_WhenRepositoryThrowsException_ShouldThrowPlaylistServiceException()
   {
       // Arrange
       int userId = 1;
       _mockPlaylistRepository.Setup(x => x.GetAllPlaylists(userId)).Throws<PlaylistRepositoryException>();

       // Act & Assert
       Assert.Throws<PlaylistServiceException>(() => _playlistService.GetAllPlaylists(userId));
   }

   [Test]
   public void GetPlaylist_WithValidId_ShouldReturnPlaylist()
   {
       // Arrange
       int playlistId = 1;
       int userId = 1;
       PlaylistWithSongsDto expectedPlaylist = new PlaylistWithSongsDto { Id = playlistId, Title = "Test Playlist" };
       _mockPlaylistRepository.Setup(x => x.GetPlaylist(playlistId, userId)).Returns(expectedPlaylist);

       // Act
       PlaylistWithSongsDto result = _playlistService.GetPlaylist(playlistId, userId);

       // Assert
       Assert.That(result, Is.EqualTo(expectedPlaylist));
   }

   [Test]
   public void GetPlaylist_WithInvalidId_ShouldThrowNotFoundException()
   {
       // Arrange
       int playlistId = 1;
       int userId = 1;
       _mockPlaylistRepository.Setup(x => x.GetPlaylist(playlistId, userId)).Returns((PlaylistWithSongsDto)null);

       // Act & Assert
       Assert.Throws<NotFoundException>(() => _playlistService.GetPlaylist(playlistId, userId));
   }

   [Test]
   public void DeleteSongFromPlaylist_ShouldCallRepository()
   {
       // Arrange
       var dto = new AddRemoveSongFromPlaylistDto { PlaylistId = 1, SongId = 1 };

       // Act
       _playlistService.DeleteSongFromPlaylist(dto);

       // Assert
       _mockPlaylistRepository.Verify(x => x.DeleteSongFromPlaylist(dto), Times.Once);
   }

   [Test]
   public void DeleteSongFromPlaylist_WhenRepositoryThrowsException_ShouldThrowPlaylistServiceException()
   {
       // Arrange
       var dto = new AddRemoveSongFromPlaylistDto { PlaylistId = 1, SongId = 1 };
       _mockPlaylistRepository.Setup(x => x.DeleteSongFromPlaylist(dto)).Throws<PlaylistRepositoryException>();

       // Act & Assert
       Assert.Throws<PlaylistServiceException>(() => _playlistService.DeleteSongFromPlaylist(dto));
   }

   [Test]
   public void AddSongToPlaylist_ShouldCallRepository()
   {
       // Arrange
       var dto = new AddRemoveSongFromPlaylistDto { PlaylistId = 1, SongId = 1 };

       // Act
       _playlistService.AddSongToPlaylist(dto);

       // Assert
       _mockPlaylistRepository.Verify(x => x.AddSongToPlaylist(dto), Times.Once);
   }

   [Test]
   public void AddSongToPlaylist_WhenRepositoryThrowsException_ShouldThrowPlaylistServiceException()
   {
       // Arrange
       var dto = new AddRemoveSongFromPlaylistDto { PlaylistId = 1, SongId = 1 };
       _mockPlaylistRepository.Setup(x => x.AddSongToPlaylist(dto)).Throws<PlaylistRepositoryException>();

       // Act & Assert
       Assert.Throws<PlaylistServiceException>(() => _playlistService.AddSongToPlaylist(dto));
   }
}