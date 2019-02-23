using System;
using System.Collections.Generic;

public class Post
{
    public string title;
    public string description;
    public List<string> tags;
    public string coverImage;
    //public DateTime date;
    public string date;
    public string path;

    private string[] dateFormats = { "dd-MM-yyyy", "d-M-yyyy", "dd-M-yyyy", "d-MM-yyyy", "dd.MM.yyyy", "d.M.yyyy", "dd.M.yyyy", "d.MM.yyyy", "dd/MM/yyyy", "d/M/yyyy", "dd/M/yyyy", "d/MM/yyyy" };
    /// <summary>
    /// Creates a new blog object.
    /// </summary>
    /// <param name="title">Title of the blog</param>
    /// <param name="description">Short description</param>
    /// <param name="texts">List of texts in the blog</param>
    /// <param name="images">List of images in the blog (paths)</param>
    public Post(string title, string description, string coverImage, string tags)
        {
        this.title = title;
        this.description = description;
        DateTime d = DateTime.Now;
        date = d.Day + "." + d.Month + "." + d.Year;
        path = "/" + d.Month + "." + d.Year + "/" + FormatTitle(this.title) + "/";
        this.coverImage = coverImage;
        this.tags = CreateTags(tags);
        }
    /// Creates a new blog object.
    /// </summary>
    /// <param name="title">Title of the blog</param>
    /// <param name="description">Short description</param>
    /// <param name="texts">List of texts in the blog</param>
    /// <param name="images">List of images in the blog (paths)</param>
    public Post(string title, string description, string path, string coverImage, string tags, string date)
        {
        this.title = title;
        this.description = description;
        DateTime d;
        DateTime.TryParseExact(date, dateFormats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out d);

        if (d == DateTime.MinValue)
            {
            d = DateTime.Now;
            }
  
        this.date = d.Day + "." + d.Month + "." + d.Year;
        this.path = path;
        this.coverImage = coverImage;//path + FormatImgPath(coverImage);
        this.tags = CreateTags(tags);
        }

    /// <summary>
    /// Creates tags from a string. Each tag should be separated from the next by a comma.
    /// </summary>
    /// <param name="tags">Tag string</param>
    /// <returns></returns>
    private List<string> CreateTags(string tags)
        {
        return new List<string>(tags.Replace(", ",",").Split(','));
        }

    private string FormatImgPath(string img)
        {
        return img.Substring(img.LastIndexOf('\\') + 1);
        }

    /// <summary>
    /// Returns the title with underscores ( "_" ) between words. 
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public string FormatTitle(string str)
        {
        return str.Replace(' ', '_');
        }

    public override string ToString()
        {
        return title + " " + path;
        }
    }
