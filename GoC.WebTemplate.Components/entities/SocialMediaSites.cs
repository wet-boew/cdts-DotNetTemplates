namespace GoC.WebTemplate.Components.Entities
{
    /// <summary>
    /// Enum that represents the social sites to be displayed when the user clicks the "Share this Page" link.
    /// The list of accepted sites can be found here: http://wet-boew.github.io/v4.0-ci/docs/ref/share/share-en.html
    /// </summary>
    /// <remarks>The enum item name must match the name expected by the Closure Template for the template to work.  The enum item name is used by the Closure Template to retreive the url, image, text for that social site</remarks>
    public enum SocialMediaSites
    {
        bitly,
        blogger,
        delicious,
        digg,
        diigo,
        email,
        facebook,
        gmail,
        googleplus,
        linkedin,
        myspace,
        pinterest,
        reddit,
        stumbleupon,
        tumblr,
        twitter,
        yahoomail
    }; //NOTE: The item names must match the parameter names expected by the Closure Template for this to work
}
