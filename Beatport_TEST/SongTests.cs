using Beatport_BLL;
using Beatport_BLL.Exceptions;
using Beatport_BLL.Interfaces;
using Beatport_BLL.Models.Dtos;
using Moq;

namespace Beatport_TEST;

public class SongTests
{
    private Mock<ISongRepository> _mockSongRepository;
    private SongService _songService;

    [SetUp]
    public void Setup()
    {
        _mockSongRepository = new Mock<ISongRepository>();
        _songService = new SongService(_mockSongRepository.Object);
    }

    [Test]
    public void GetAllSongs_ShouldReturnListOfSongs()
    {
        // Arrange
        List<SongDto> expectedSongs = new List<SongDto>
        {
            new() { Id = 1, Title = "Test Song 1" },
            new() { Id = 2, Title = "Test Song 2" }
        };
        _mockSongRepository.Setup(x => x.GetAllSongs()).Returns(expectedSongs);

        // Act
        List<SongDto> result = _songService.GetAllSongs();

        // Assert
        Assert.That(result, Is.EqualTo(expectedSongs)); 
    }

    [Test]
    public void GetAllSongs_WhenRepositoryThrowsException_ShouldThrowSongServiceException()
    {
        // Arrange
        _mockSongRepository.Setup(x => x.GetAllSongs()).Throws<SongRepositoryException>();

        // Act & Assert
        Assert.Throws<SongServiceException>(() => _songService.GetAllSongs());
    }

    [Test]
    public void GetSong_WithValidId_ShouldReturnSong()
    {
        // Arrange
        SongDto expectedSong = new SongDto { Id = 1, Title = "Test Song" };
        _mockSongRepository.Setup(x => x.GetSong(1)).Returns(expectedSong);

        // Act
        SongDto result = _songService.GetSong(1);

        // Assert
        Assert.That(result, Is.EqualTo(expectedSong));
    }

    [Test]
    public void GetSong_WithInvalidId_ShouldThrowNotFoundException()
    {
        // Arrange
        _mockSongRepository.Setup(x => x.GetSong(1)).Returns((SongDto)null);

        // Act & Assert
        Assert.Throws<NotFoundException>(() => _songService.GetSong(1));
    }

    [Test]
    public void CreateSong_WithValidData_ShouldReturnTrue()
    {
        // Arrange
        CreateEditSongDto songDto = new CreateEditSongDto { Title = "New Song" };
        _mockSongRepository.Setup(x => x.CreateSong(songDto)).Returns(true);

        // Act
        bool result = _songService.CreateSong(songDto);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CreateSong_WhenRepositoryThrowsException_ShouldThrowSongServiceException()
    {
        // Arrange
        CreateEditSongDto songDto = new CreateEditSongDto { Title = "New Song" };
        _mockSongRepository.Setup(x => x.CreateSong(songDto)).Throws<SongRepositoryException>();

        // Act & Assert
        Assert.Throws<SongServiceException>(() => _songService.CreateSong(songDto));
    }

    [Test]
    public void EditSong_WithValidData_ShouldReturnTrue()
    {
        // Arrange
        CreateEditSongDto songDto = new CreateEditSongDto { Title = "Updated Song" };
        _mockSongRepository.Setup(x => x.GetSong(1)).Returns(new SongDto { Id = 1 });
        _mockSongRepository.Setup(x => x.EditSong(1, songDto)).Returns(true);

        // Act
        bool result = _songService.EditSong(1, songDto);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void EditSong_WithInvalidId_ShouldThrowNotFoundException()
    {
        // Arrange
        CreateEditSongDto songDto = new CreateEditSongDto { Title = "Updated Song" };
        _mockSongRepository.Setup(x => x.GetSong(1)).Returns((SongDto)null);

        // Act & Assert
        Assert.Throws<NotFoundException>(() => _songService.EditSong(1, songDto));
    }

    [Test]
    public void DeleteSong_WithValidId_ShouldReturnTrue()
    {
        // Arrange
        _mockSongRepository.Setup(x => x.GetSong(1)).Returns(new SongDto { Id = 1 });
        _mockSongRepository.Setup(x => x.DeleteSong(1)).Returns(true);

        // Act
        bool result = _songService.DeleteSong(1);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void DeleteSong_WithInvalidId_ShouldThrowNotFoundException()
    {
        // Arrange
        _mockSongRepository.Setup(x => x.GetSong(1)).Returns((SongDto)null);

        // Act & Assert
        Assert.Throws<NotFoundException>(() => _songService.DeleteSong(1));
    }
}