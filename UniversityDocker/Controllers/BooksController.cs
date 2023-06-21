using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityDocker.Entities;

namespace UniversityDocker.Controllers;

[ApiController]
[Route("[controller]")]
public class BooksController : ControllerBase
{
    private readonly DbContextFactory _dbContextFactory;

    public BooksController(DbContextFactory dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    [HttpGet]
    public async Task<IEnumerable<Book>> GetAll()
    {
        var dbContext = _dbContextFactory.Create();

        return await dbContext.Books.ToListAsync();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Book>> GetById(Guid id)
    {
        var dbContext = _dbContextFactory.Create();

        var book = await dbContext.Books.SingleOrDefaultAsync(x => x.Id == id);
        return book == null ? NotFound() : Ok(book);
    }

    [HttpPost]
    public async Task<ActionResult<Book>> Insert(Book book)
    {
        var dbContext = _dbContextFactory.Create();

        book.Id = Guid.NewGuid();
        var res = await dbContext.Books.AddAsync(book);
        
        await dbContext.SaveChangesAsync();

        return Ok(res.Entity);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<Book>> Update([FromRoute] Guid id, [FromBody] Book book)
    {
        var dbContext = _dbContextFactory.Create();

        var foundBook = await dbContext.Books.SingleOrDefaultAsync(x => x.Id == id);
        if (foundBook == null)
            return NotFound();

        dbContext.Books.Remove(foundBook);
        var res = await dbContext.Books.AddAsync(book);
        await dbContext.SaveChangesAsync();

        return Ok(res.Entity);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Remove(Guid id)
    {
        var dbContext = _dbContextFactory.Create();

        var book = await dbContext.Books.SingleOrDefaultAsync(x => x.Id == id);
        if (book == null)
            return NotFound();

        dbContext.Books.Remove(book);
        await dbContext.SaveChangesAsync();

        return Ok();
    }
}