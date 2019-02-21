using System;

public class Blog
{
    public string _title;
    public string _description;
    public string _coverImage;
    public DateTime _date;
    public string _path;
    public string _html;
    /// <summary>
    /// Creates a new blog object.
    /// </summary>
    /// <param name="title">Title of the blog</param>
    /// <param name="description">Short description</param>
    /// <param name="texts">List of texts in the blog</param>
    /// <param name="images">List of images in the blog (paths)</param>
    public Blog(string title, string description, string coverImage)
        {
        _title = title;
        _description = description;
        _date = DateTime.Now;
        _path = "/" + _date.Month + "." + _date.Year + "/" + FormatTitle(_title) + "/";
        _html = _path + FormatTitle(_title) + ".html";
        _coverImage = _path + FormatImgPath(coverImage);
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
        return _title + " " + _path;
        }
    }
