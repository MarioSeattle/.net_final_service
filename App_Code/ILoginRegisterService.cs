using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ILoginRegisterService" in both code and config file together.
[ServiceContract]
public interface ILoginRegisterService
{
    [OperationContract]
    int LoginVenue(string VenueLoginUserName, string VenueLoginPasswordPlain);

    [OperationContract]
    bool VenueRegister(VenueData v);

    [OperationContract]
    bool AddArtist(Artist a);

    [OperationContract]
    bool AddShow(Show s, ShowDetail sd);

    [OperationContract]
    int LoginFan(string FanLoginUserName, string FanLoginPasswordPlain);

    [OperationContract]
    bool FanRegister(string FanName, string FanEmail, string fanUsername, string fanPassword);

    [OperationContract]
    int AddFanArtist(int fanKey, string artistName);
}



[DataContract]
    public class VenueData
    {
        [DataMember]
        public string VenueName { set; get; }

        [DataMember]
        public string VenueAddress { set; get; }//uppercase because it's instance


    [DataMember]
    public string VenueCity { set; get; }

    [DataMember]
    public string VenueState { set; get; }

  

    [DataMember]
    public string VenueZipCode { set; get; }

    [DataMember]
    public string VenuePhone { set; get; }

    [DataMember]
    public string VenueEmail { set; get; }

    [DataMember]
    public string VenueWebPage { set; get; }

    [DataMember]
    public int VenueAgeRestriction { set; get; }

    [DataMember]
    public string VenueLoginUserName { set; get; }

    [DataMember]
    public string VenueLoginPasswordPlain { set; get; }


}
