using System;

public class Blog
{
    public string title;
    public string description;
    public string coverImage;
    //public DateTime date;
    public string date;
    public string path;
    /// <summary>
    /// Creates a new blog object.
    /// </summary>
    /// <param name="title">Title of the blog</param>
    /// <param name="description">Short description</param>
    /// <param name="texts">List of texts in the blog</param>
    /// <param name="images">List of images in the blog (paths)</param>
    public Blog(string title, string description, string coverImage)
        {
        this.title = title;
        this.description = description;
        DateTime d = DateTime.Now;
        date = d.Day + "." + d.Month + "." + d.Year;
        path = "/" + d.Month + "." + d.Year + "/" + FormatTitle(this.title) + "/";
        this.coverImage = coverImage;
        }
    /// Creates a new blog object.
    /// </summary>
    /// <param name="title">Title of the blog</param>
    /// <param name="description">Short description</param>
    /// <param name="texts">List of texts in the blog</param>
    /// <param name="images">List of images in the blog (paths)</param>
    public Blog(string title, string description, string path, string coverImage)
        {
        this.title = title;
        this.description = description;
        DateTime d = DateTime.Now;
        date = d.Day + "." + d.Month + "." + d.Year;
        this.path = path;
        this.coverImage = coverImage;//path + FormatImgPath(coverImage);
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
