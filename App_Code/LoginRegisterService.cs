using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "LoginRegisterService" in code, svc and config file together.
public class LoginRegisterService : ILoginRegisterService
{
    ShowTrackerEntities db = new ShowTrackerEntities();

    public bool AddArtist(Artist a)
    {
        Artist artist = new Artist();
        artist.ArtistName = a.ArtistName;
        artist.ArtistDateEntered = DateTime.Now;
        artist.ArtistEmail = a.ArtistEmail;
        bool result = true;
        try
        {
            db.Artists.Add(artist);
            db.SaveChanges();
        }

        catch (Exception ex)
        {
            result = false;
            throw ex;
        }

        return result;
    }

    public bool AddShow(Show s, ShowDetail sd)
    {
        Show show = new Show();
        show.ShowName = s.ShowName;
        show.ShowDateEntered = s.ShowDateEntered;
        show.ShowDate = s.ShowDate;
        show.ShowTicketInfo = s.ShowTicketInfo;
        show.ShowTime = s.ShowTime;

        ShowDetail showDetail = new ShowDetail();
        showDetail.ShowDetailAdditional = sd.ShowDetailAdditional;
        showDetail.ShowDetailArtistStartTime = sd.ShowDetailArtistStartTime;

        showDetail.Show = show;

        bool result = true;

        
        try
        {
            db.Shows.Add(show);
            db.ShowDetails.Add(showDetail);
            db.SaveChanges();
        }

        catch (Exception ex)
        {
            result = false;
            throw ex;
        }

        return result;

    }

    public int LoginFan(string FanLoginUserName, string FanLoginPasswordPlain)
    {
        int fanKey = 0;
        int result = db.usp_FanLogin(FanLoginUserName, FanLoginPasswordPlain);
        if (result != -1)
        {
            var key = (from f in db.FanLogins
                       where f.FanLoginUserName.Equals(FanLoginUserName)
                       select new { f.FanKey }).FirstOrDefault();
            fanKey = (int)key.FanKey;

        }
        return fanKey;
    }

    public int LoginVenue(string VenueLoginUserName, string VenueLoginPasswordPlain)
    {
        int VenueKey = 0;
        int result = db.usp_venueLogin(VenueLoginUserName, VenueLoginPasswordPlain);
        if (result != -1)
        {
            var key = (from v in db.VenueLogins
                       where v.VenueLoginUserName.Equals(VenueLoginUserName)
                       select new { v.VenueKey }).FirstOrDefault();// returns the first one , because key is not seen as a single value
            // the key contains "Reviewer  = key", so that's why we need the key.ReviewerKey
            VenueKey = (int)key.VenueKey;

        }
        return VenueKey;
    }

    public bool FanRegister(string fanName, string fanEmail,string fanUsername, string fanPassword)
    {
        bool result = true;
        int pass = db.usp_RegisterFan(fanName, fanEmail,fanUsername,fanPassword);
        if (pass == -1)
        {
            result = false;
        }

        return result;
    }

    public bool VenueRegister(VenueData v)
    { bool result = true;
        int pass = db.usp_RegisterVenue(v.VenueName,v.VenueAddress,v.VenueCity,v.VenueState,v.VenueZipCode,v.VenuePhone,v.VenueEmail,v.VenueWebPage,v.VenueAgeRestriction,v.VenueLoginUserName,v.VenueLoginPasswordPlain);
        if (pass == -1)
        {
            result = false;
        }

        return result;
        
    }

    public int AddFanArtist(int fanKey, string artistName)
    {
        /*********************************
         * This method will add an artist to the artistFan
         * table. First we have to find the fan
         * and then the particular artist
         * Then we add the artist to the Fan's list
         * of artists to follow
         * **********************************/
        int result = 1;

        //get the fan. the key can come from their login
        Fan myFan = (from f in db.Fans
                     where f.FanKey == fanKey
                     select f).First();

        //get the artist by name
        Artist myArtist = (from a in db.Artists
                           where a.ArtistName.Equals(artistName)
                           select a).First();

        //add the artist to the fan;'s collection of artists
        myFan.Artists.Add(myArtist);

        //save the changes
        db.SaveChanges();

        return result;
    }
}

