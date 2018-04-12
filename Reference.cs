using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

/// <copyright>2018 Trak iT Wireless Inc.</copyright>
/// <summary>
/// The WebSocket provides access to the majority of the system.
/// This helper file is intended to help you develop your own Microsoft .NET solution by
/// providing type, class, and data structure definitions that follow the WebSocket's
/// command structure.
/// Last updated on Thursday Apr 12 2018 12:13:00
/// </summary>
/// <seealso cref="https://github.com/fleetfreedom/WhiteWhale/" />
/// <seealso cref="https://apis.fleetfreedom.com/wss/reference/" />
/// <version>4.10.2</version>
namespace trakit.fleetfreedom {
	#region Common Formats
	/// <summary>
	/// A lower-case string with all the apostrophes and quotation marks [", ', `, “, and ”] removed,
	/// and all non-alphanumeric character sequences replaced with a single minus sign.
	/// Diacritics such as \u00ae, \u00e9, and \u00e7 are also replaced with their alhpabetic equivalents (r, e, and c).
	/// </summary>
	public struct codified {
		private readonly string _value;
		public codified(string value) => _value = value;
		public bool Equals(codified other) => _value == other._value;
		public static implicit operator codified(string d) => d;
		public static implicit operator string(codified d) => d._value;
		public static bool operator ==(codified a, codified b) => a.Equals(b);
		public static bool operator !=(codified a, codified b) => !a.Equals(b);
		public override int GetHashCode() => _value.GetHashCode();
		public override bool Equals(object obj) => obj is codified && this.Equals((codified)obj);
		public override string ToString() => _value.ToString();
	}
	/// <summary>
	/// A serialized-string representation of a duration of time. There are two possible formats when using a duration string.
  	/// A string formatted like a duration of time. The format patterns are:
	/// [-][d.]H:mm[:ss[.fff]] or [-][d]
	/// </summary>
	public struct duration {
		private readonly TimeSpan _value;
		public duration(string value) => _value = TimeSpan.Parse(value);
		public bool Equals(duration other) => _value == other._value;
		public static implicit operator duration(TimeSpan d) => d;
		public static implicit operator TimeSpan(duration d) => d._value;
		public static bool operator ==(duration a, duration b) => a.Equals(b);
		public static bool operator !=(duration a, duration b) => !a.Equals(b);
		public override int GetHashCode() => _value.GetHashCode();
		public override bool Equals(object obj) => obj is duration && this.Equals((duration)obj);
		public override string ToString() => _value.ToString();
	}
	/// <summary>
	/// An email address identifies a mailbox to which messages are delivered.
	/// We follow the RFC 2822 patter for addresses.
	/// The format pattern is:
	/// user@domain.tld
	/// </summary>
	public struct email {
		private readonly MailAddress _value;
		public email(string value) => _value = new MailAddress(value);
		public bool Equals(email other) => _value == other._value;
		public static implicit operator email(MailAddress d) => d;
		public static implicit operator MailAddress(email d) => d._value;
		public static bool operator ==(email a, email b) => a.Equals(b);
		public static bool operator !=(email a, email b) => !a.Equals(b);
		public override int GetHashCode() => _value.GetHashCode();
		public override bool Equals(object obj) => obj is email && this.Equals((email)obj);
		public override string ToString() => _value.ToString();
	}
	/// <summary>
	/// A targeting pattern is a string that represents an in-the-moment search for objects.
	/// The operators used depend entirely on the context of the search, and compatibility with the sub-system.
	/// The format pattern is:
	/// [operator:terms] terms
	/// </summary>
	public struct expression {
		private readonly string _value;
		public expression(string value) => _value = value;
		public bool Equals(expression other) => _value == other._value;
		public static implicit operator expression(string d) => d;
		public static implicit operator string(expression d) => d._value;
		public static bool operator ==(expression a, expression b) => a.Equals(b);
		public static bool operator !=(expression a, expression b) => !a.Equals(b);
		public override int GetHashCode() => _value.GetHashCode();
		public override bool Equals(object obj) => obj is expression && this.Equals((expression)obj);
		public override string ToString() => _value.ToString();
	}
	/// <summary>
	/// An IP address (IPv4 only) or range using CIDR notation.
	/// The format pattern is:
	/// n.n.n.n[/r]
	/// </summary>
	public struct ipv4 {
		private readonly IPAddress _value;
		public ipv4(string value) => _value = IPAddress.Parse(value);
		public bool Equals(ipv4 other) => _value == other._value;
		public static implicit operator ipv4(IPAddress d) => d;
		public static implicit operator IPAddress(ipv4 d) => d._value;
		public static bool operator ==(ipv4 a, ipv4 b) => a.Equals(b);
		public static bool operator !=(ipv4 a, ipv4 b) => !a.Equals(b);
		public override int GetHashCode() => _value.GetHashCode();
		public override bool Equals(object obj) => obj is ipv4 && this.Equals((ipv4)obj);
		public override string ToString() => _value.ToString();
	}
	/// <summary>
	/// An implementation of {@link Google's Encoded Polyline|https://developers.google.com/maps/documentation/utilities/polylinealgorithm} algorithm.
	/// The algorithm is a lossy compression that stores a series of coordinates as a
	/// single string.  All coordinates are rounded to five decimal places before
	/// encoding, but this should be accurate to within roughly one and a half meters.
	/// Full details can be found on Google's Maps API site, as well as a utility to 
	/// {@link test encoding and decoding|https://developers.google.com/maps/documentation/utilities/polylineutility}.
	/// </summary>
	public struct polyline {
		private readonly string _value;
		public polyline(string value) => _value = value;
		public bool Equals(polyline other) => _value == other._value;
		public static implicit operator polyline(string d) => d;
		public static implicit operator string(polyline d) => d._value;
		public static bool operator ==(polyline a, polyline b) => a.Equals(b);
		public static bool operator !=(polyline a, polyline b) => !a.Equals(b);
		public override int GetHashCode() => _value.GetHashCode();
		public override bool Equals(object obj) => obj is polyline && this.Equals((polyline)obj);
		public override string ToString() => _value.ToString();
	}
	#endregion Common Formats

	#region API Definitions
	/// <summary>
	/// The possible errors returned for SocketResponses.
	/// </summary>
	public enum ErrorCode {
		/// <summary> 
		/// Success
		/// Operation completed without error
		/// </summary>
		success = 0,
		/// <summary> 
		/// Unknown error
		/// </summary>
		unknown = 1,
		/// <summary> 
		/// Internal service error
		/// Service error not because of client input
		/// </summary>
		service = 2,
		/// <summary> 
		/// Invalid data
		/// Client input data was not parsed properly
		/// </summary>
		invalidData = 3,
		/// <summary> 
		/// Invalid permission
		/// Not a known permission
		/// </summary>
		invalidPermission = 4,
		/// <summary> 
		/// Permission denied
		/// Operation not successful because access is restricted
		/// </summary>
		permissionDenied = 5,
		/// <summary> 
		/// Incorrect version key
		/// Version keys sent with request do not match service version. In these cases, treat the service version as most recent.
		/// </summary>
		wrongVersionKey = 6,
		/// <summary> 
		/// Session expired
		/// User's session is expired or unknown
		/// </summary>
		sessionExpired = 7,
		/// <summary> 
		/// Please login
		/// Please login before all other operations
		/// </summary>
		userNotLoggedIn = 8,
		/// <summary> 
		/// Session was forcibly killed
		/// User's session was ended by another user, or by company's session policy
		/// </summary>
		sessionKilled = 9,
		/// <summary> 
		/// Logged out
		/// The session has ended
		/// </summary>
		loggedOut = 10,
		/// <summary> 
		/// Invalid credentials
		/// Is your caps-lock on?
		/// </summary>
		invalidCredentials = 11,
		/// <summary> 
		/// Application not allowed
		/// The given UserAgent is not allowed
		/// </summary>
		applicationNotAllowed = 12,
		/// <summary> 
		/// IPAddress not allowed
		/// IP restrictions are in place, and the client IP is not in the allowed list
		/// </summary>
		ipNotAllowed = 13,
		/// <summary> 
		/// Session active from another client
		/// Session is active from another live connection
		/// </summary>
		multiUserDenied = 14,
		/// <summary> 
		/// Password doesn't comply with the password policy
		/// The PasswordPolicy is a part of the Company object, and the connectionResponse for your own user.
		/// </summary>
		noncompliantPassword = 15,
		/// <summary> 
		/// Password expired; please change your password
		/// An expired password does not mean your session has expired. If you create a new session, it will ask you to update your password again.
		/// </summary>
		passwordExpired = 16,
		/// <summary> 
		/// New password must be different
		/// When changing a password, it must not be the same as the previous password.
		/// </summary>
		samePassword = 17,
		/// <summary> 
		/// Kill session failed
		/// Unable to end the session
		/// </summary>
		killSessionFailed = 18,
		/// <summary> 
		/// Session not found
		/// </summary>
		sessionNotFound = 19,
		/// <summary> 
		/// Asset not found
		/// </summary>
		assetNotFound = 20,
		/// <summary> 
		/// Asset not deleted
		/// </summary>
		assetNotDeleted = 21,
		/// <summary> 
		/// One or more assets in the list not found
		/// </summary>
		assetsListNotFound = 22,
		/// <summary> 
		/// Behaviour script not found
		/// </summary>
		behaviourScriptNotFound = 23,
		/// <summary> 
		/// Behaviour script not deleted
		/// </summary>
		behaviourScriptNotDeleted = 24,
		/// <summary> 
		/// Behaviour script currently in use by one or more behaviours
		/// In order to delete a Behaviour Script, all Behaviours implementing this script must be deleted first.
		/// </summary>
		behaviourScriptInUse = 25,
		/// <summary> 
		/// Behaviour not found
		/// </summary>
		behaviourNotFound = 26,
		/// <summary> 
		/// Behaviour not deleted
		/// </summary>
		behaviourNotDeleted = 27,
		/// <summary> 
		/// Company not found
		/// </summary>
		companyNotFound = 28,
		/// <summary> 
		/// Company not deleted
		/// </summary>
		companyNotDeleted = 29,
		/// <summary> 
		/// User group not found
		/// </summary>
		userGroupNotFound = 30,
		/// <summary> 
		/// Contact not found
		/// </summary>
		contactNotFound = 31,
		/// <summary> 
		/// Contact not deleted
		/// </summary>
		contactNotDeleted = 32,
		/// <summary> 
		/// Icon not found
		/// </summary>
		iconNotFound = 33,
		/// <summary> 
		/// Icon not deleted
		/// </summary>
		iconNotDeleted = 34,
		/// <summary> 
		/// Maintenance job not found
		/// </summary>
		maintenanceJobNotFound = 35,
		/// <summary> 
		/// Maintenance job not deleted
		/// </summary>
		maintenanceJobNotDeleted = 36,
		/// <summary> 
		/// Maintenance schedule not found
		/// </summary>
		maintenanceScheduleNotFound = 37,
		/// <summary> 
		/// Maintenance schedule not deleted
		/// </summary>
		maintenanceScheduleNotDeleted = 38,
		/// <summary> 
		/// Maintenance schedule currently in use by one or more maintenance jobs
		/// In order to delete a Maintenance Schedule, all Maintenance Jobs referencing this schedule must be deleted first.
		/// </summary>
		maintenanceScheduleInUse = 39,
		/// <summary> 
		/// Place not found
		/// </summary>
		placeNotFound = 40,
		/// <summary> 
		/// Place not deleted
		/// </summary>
		placeNotDeleted = 41,
		/// <summary> 
		/// One or more places in the list not found
		/// </summary>
		placesListNotFound = 42,
		/// <summary> 
		/// Provider not found
		/// </summary>
		providerNotFound = 43,
		/// <summary> 
		/// Provider not deleted
		/// </summary>
		providerNotDeleted = 44,
		/// <summary> 
		/// One or more providers in the list not found
		/// </summary>
		providersListNotFound = 45,
		/// <summary> 
		/// Cannot access deleted provider
		/// </summary>
		deletedProvider = 46,
		/// <summary> 
		/// Provider configuration template not found
		/// </summary>
		providerConfigurationTypeNotFound = 47,
		/// <summary> 
		/// Provider configuration not found
		/// </summary>
		providerConfigurationNotFound = 48,
		/// <summary> 
		/// Provider configuration not deleted
		/// </summary>
		providerConfigurationNotDeleted = 49,
		/// <summary> 
		/// Provider configuration currently in use by one or more providers
		/// In order to delete a Provider Configuration, all Providers using this configuration must be deleted first, or have their configuration changed.
		/// </summary>
		providerConfigurationInUse = 50,
		/// <summary> 
		/// Invalid Provider configuration options
		/// </summary>
		invalidConfigurationOptions = 51,
		/// <summary> 
		/// Report Template not found
		/// </summary>
		reportTemplateNotFound = 52,
		/// <summary> 
		/// Report Template not deleted
		/// </summary>
		reportTemplateNotDeleted = 53,
		/// <summary> 
		/// User not found
		/// </summary>
		userNotFound = 54,
		/// <summary> 
		/// User not deleted
		/// </summary>
		userNotDeleted = 55,
		/// <summary> 
		/// Cannot access deleted user
		/// </summary>
		deletedUser = 56,
		/// <summary> 
		/// Cannot delete yourself
		/// Don't do it! You have so much to live for!
		/// </summary>
		suicide = 57,
		/// <summary> 
		/// LabelStyle code names must be unique
		/// </summary>
		labelsListNotUnique = 58,
		/// <summary> 
		/// TagStyle code names must be unique
		/// </summary>
		tagsListNotUnique = 59,
		/// <summary> 
		/// One or more User groups in the list not found
		/// </summary>
		userGroupsListNotFound = 60,
		/// <summary> 
		/// Unknown command
		/// What are you trying to do genius?
		/// </summary>
		unknownCommand = 61,
		/// <summary> 
		/// Timezone not found
		/// A full list of valid timezones can be retrieved using getTimezonesList.
		/// </summary>
		timezoneNotFound = 62,
		/// <summary> 
		/// Asset message not found
		/// </summary>
		assetMessageNotFound = 63,
		/// <summary> 
		/// Dispatch task not found
		/// </summary>
		dispatchTaskNotFound = 64,
		/// <summary> 
		/// User group currently in use by one or more users
		/// </summary>
		userGroupInUse = 65,
		/// <summary> 
		/// User group not deleted
		/// </summary>
		userGroupNotDeleted = 66,
		/// <summary> 
		/// Report Result not found
		/// </summary>
		reportResultNotFound = 67,
		/// <summary> 
		/// Picture not found
		/// </summary>
		pictureNotFound = 68,
		/// <summary> 
		/// One or more pictures in the list not found
		/// </summary>
		picturesListNotFound = 69,
		/// <summary> 
		/// Picture not deleted
		/// </summary>
		pictureNotDeleted = 70,
		/// <summary> 
		/// Permission escalation
		/// Unable to perform operation because results would escalate user's permissions
		/// </summary>
		permissionEscalation = 71,
		/// <summary> 
		/// User with this login already exists
		/// </summary>
		userAlreadyExists = 72,
		/// <summary> 
		/// Session throttled
		/// Unable to create a new connection due to session throttling
		/// </summary>
		sessionThrottled = 73,
		/// <summary> 
		/// One or more users in the list not found
		/// </summary>
		usersListNotFound = 74,
		/// <summary> 
		/// Report Result not deleted
		/// </summary>
		reportResultNotDeleted = 75,
		/// <summary> 
		/// Carrier not found
		/// </summary>
		carrierNotFound = 76,
		/// <summary> 
		/// Carrier not deleted
		/// </summary>
		carrierNotDeleted = 77,
		/// <summary> 
		/// Trip Inspection not found
		/// </summary>
		inspectionNotFound = 78,
		/// <summary> 
		/// Trip Inspection not deleted
		/// </summary>
		inspectionNotDeleted = 79,
		/// <summary> 
		/// There was an error retrieving the address' coordinates
		/// Check the errorDetails for more information.
		/// </summary>
		geocoderError = 80,
		/// <summary> 
		/// No coordinates could be found for the given address
		/// Ensure the address is correct, or try again without postal code.
		/// </summary>
		geocoderNotFound = 81,
		/// <summary> 
		/// There was an error calculating the directions or too many stops were given
		/// Check the errorDetails for more information.
		/// </summary>
		directionsError = 82,
		/// <summary> 
		/// Directions could not be calculated between two or more stops
		/// Ensure the each stop is correct, or try a shorter route.
		/// </summary>
		directionsNotFound = 83,
		/// <summary> 
		/// Driver E-Log not found
		/// </summary>
		elogNotFound = 84
	}

	/// <summary> 
	/// A coordinate on the globe
	/// </summary>
	public partial class LatLng {
		/// <summary>
		/// Latitude
		/// </summary>
		public double lat;
		/// <summary>
		/// Longitude
		/// </summary>
		public double lng;
	}

	/// <summary> 
	/// A boundary on the globe
	/// </summary>
	public partial class LatLngBounds {
		/// <summary>
		/// Eastern longitude
		/// </summary>
		public double east;
		/// <summary>
		/// Northern latitude
		/// </summary>
		public double north;
		/// <summary>
		/// Southern latitude
		/// </summary>
		public double south;
		/// <summary>
		/// Western longitude
		/// </summary>
		public double west;
	}

	/// <summary> 
	/// A coordinate on a flat surface
	/// </summary>
	public partial class Point {
		/// <summary>
		/// Horizontal coordinate
		/// </summary>
		public double x;
		/// <summary>
		/// Vertical coordinate
		/// </summary>
		public double y;
	}

	/// <summary> 
	/// Dimensions on a flat surface
	/// </summary>
	public partial class Size {
		/// <summary>
		/// Height
		/// </summary>
		public double height;
		/// <summary>
		/// Width
		/// </summary>
		public double width;
	}

	/// <summary> 
	/// A boundary on a flat surface
	/// </summary>
	public partial class Square {
		/// <summary>
		/// Lowest vertical coordinate
		/// </summary>
		public double bottom;
		/// <summary>
		/// Left-most horizontal coordinate
		/// </summary>
		public double left;
		/// <summary>
		/// Right-most horizontal coordinate
		/// </summary>
		public double right;
		/// <summary>
		/// Highest vertical coordinate
		/// </summary>
		public double top;
	}

	/// <summary> 
	/// Timezone definition
	/// </summary>
	public partial class Timezone {
		/// <summary>
		/// Unique timezone code
		/// </summary>
		public string code;
		/// <summary>
		/// Indicates whether this timezone abides by daylight savings
		/// </summary>
		public bool dst;
		/// <summary>
		/// Common timezone name
		/// </summary>
		public string name;
		/// <summary>
		/// Minutes offset from GMT
		/// </summary>
		public ushort offset;
	}
	#endregion API Definitions
	#region Assets
	/// <summary>
	/// The kinds of interactions had with a Place.
	/// </summary>
	public enum AssetPlaceStatusType {
		/// <summary> 
		/// Occurs when an asset is outside a Place, then goes inside the boundary.
		/// </summary>
		enter,
		/// <summary> 
		/// Occurs when an asset was inside the boundary of a Place, but then moves outside the boundary.
		/// </summary>
		exit,
		/// <summary> 
		/// Occurs when the asset was inside the boundary before, and is still inside the boundary now.
		/// </summary>
		inside
	}

	/// <summary>
	/// The four supported types of trackable things.
	/// </summary>
	public enum AssetType {
		/// <summary> 
		/// Generic thing.
		/// </summary>
		asset,
		/// <summary> 
		/// Human (or alien) Person.
		/// </summary>
		person,
		/// <summary> 
		/// A towed vehicle without an engine.
		/// </summary>
		trailer,
		/// <summary> 
		/// A vehicle that moves with its own power.
		/// </summary>
		vehicle
	}

	/// <summary> 
	/// The full details of a Person, Vehicle, Trailer, or Asset. Not all properties are present for all asset types. Depending on the kind, the asset will contain properties from the PersonGeneral, VehicleGeneral, TrailerGeneral, or AssetGeneral object. For vehicle type assets, the asset will contain properties from the VehicleAdvanced object, but all other asset types will contain properties from the AssetAdvanced object.
	/// </summary>
	public partial class Asset {
		/// <summary>
		/// A list of attributes given to this asset by the connection device such as wiring state, VBus, etc.
		/// The object's keys follow the codified format.
		/// </summary>
		/// <seealso cref="codified" />
		public Dictionary<codified, AssetAttribute> attributes;
		/// <summary>
		/// Primary colour of the trailer (given in 24bit hex; #RRGGBB)
		/// </summary>
		/// <remarks>max-length: 22</remarks>
		public string colour;
		/// <summary>
		/// The company to which this asset belongs.
		/// </summary>
		public ulong company;
		/// <summary>
		/// A reference to their Company's Contact information.
		/// </summary>
		public ulong contact;
		/// <summary>
		/// The cumulative duration that the vehicle's engine has been running (in decimal hours).
		/// </summary>
		public double engineHours;
		/// <summary>
		/// The icon that represents this asset on the map and in lists.
		/// </summary>
		public ulong icon;
		/// <summary>
		/// Unique identifier of this asset.
		/// </summary>
		public ulong id;
		/// <summary>
		/// Type of asset.
		/// </summary>
		public AssetType kind;
		/// <summary>
		/// Codified label names.
		/// </summary>
		public string[] labels;
		/// <summary>
		/// Manufacturer's name.
		/// </summary>
		/// <remarks>max-length: 50</remarks>
		public string make;
		/// <summary>
		/// The fall-back address which is used to send Messages if the asset is a Person and has no Contact phone or email.
		/// </summary>
		/// <remarks>max-length: 254</remarks>
		public string messagingAddress;
		/// <summary>
		/// Manufacturer's model name/number.
		/// </summary>
		/// <remarks>max-length: 50</remarks>
		public string model;
		/// <summary>
		/// This thing's name.
		/// </summary>
		/// <remarks>max-length: 100</remarks>
		public string name;
		/// <summary>
		/// Notes about it.
		/// </summary>
		public string notes;
		/// <summary>
		/// The cumulative distance travelled in kilometres.
		/// </summary>
		public double odometer;
		/// <summary>
		/// A list of photos of this thing.
		/// </summary>
		public ulong[] pictures;
		/// <summary>
		/// The current state of this asset's interaction with known Places.
		/// The object's keys follow the uint64 format.
		/// </summary>
		/// <seealso cref="uint64" />
		public Dictionary<ulong, AssetPlaceStatus> places;
		/// <summary>
		/// The license plate.
		/// </summary>
		/// <remarks>max-length: 50</remarks>
		public string plate;
		/// <summary>
		/// The things GPS coordinates including speed, bearing, and street information.
		/// </summary>
		public Position position;
		/// <summary>
		/// The list of devices providing events for this asset.
		/// </summary>
		public string[] providers;
		/// <summary>
		/// A custom field used to refer to an external system.
		/// </summary>
		/// <remarks>max-length: 100</remarks>
		public string reference;
		/// <summary>
		/// A list of assets related to this one; like a Person for a Vehicle (driver).
		/// </summary>
		public ulong[] relationships;
		/// <summary>
		/// Manufacturer's unique identification number for this trailer.
		/// </summary>
		/// <remarks>max-length: 50</remarks>
		public string serial;
		/// <summary>
		/// The codified status tag names.
		/// </summary>
		public string[] tags;
		/// <summary>
		/// Object version keys used to validate synchronization for different properties.
		/// v[0]: Properties found in the AssetGeneral object.
		/// v[1]: Properties found in the AssetAdvanced object.
		/// </summary>
		/// <remarks>length: 2</remarks>
		public uint[] v;
		/// <summary>
		/// Manufacturer's unique identification number (Vehicle Identification Number).
		/// </summary>
		/// <remarks>max-length: 50</remarks>
		public string vin;
		/// <summary>
		/// Year of manufacturing.
		/// </summary>
		public ushort year;
	}

	/// <summary> 
	/// Often changing details about a thing.
	/// </summary>
	public partial class AssetAdvanced {
		/// <summary>
		/// A list of attributes given to this asset by the connection device such as wiring state, VBus, etc.
		/// The object's keys follow the codified format.
		/// </summary>
		/// <seealso cref="codified" />
		public Dictionary<codified, AssetAttribute> attributes;
		/// <summary>
		/// The company to which this asset belongs.
		/// </summary>
		public ulong company;
		/// <summary>
		/// Unique identifier of this asset.
		/// </summary>
		public ulong id;
		/// <summary>
		/// The cumulative distance travelled in kilometres.
		/// </summary>
		public double odometer;
		/// <summary>
		/// The current state of this asset's interaction with known Places.
		/// The object's keys follow the uint64 format.
		/// </summary>
		/// <seealso cref="uint64" />
		public Dictionary<ulong, AssetPlaceStatus> places;
		/// <summary>
		/// The things GPS coordinates including speed, bearing, and street information.
		/// </summary>
		public Position position;
		/// <summary>
		/// The list of devices providing events for this asset.
		/// </summary>
		public string[] providers;
		/// <summary>
		/// A list of assets related to this one; like a Person for a Vehicle (driver).
		/// </summary>
		public ulong[] relationships;
		/// <summary>
		/// The codified status tag names.
		/// </summary>
		public string[] tags;
		/// <summary>
		/// Object version keys used to validate synchronization for all object properties.
		/// </summary>
		public uint[] v;
	}

	/// <summary> 
	/// An attribute given to an asset by a behaviour script.
	/// </summary>
	public partial class AssetAttribute {
		/// <summary>
		/// The related asset which provided this attribute.
		/// </summary>
		public ulong asset;
		/// <summary>
		/// Parse-able/formatted string for complex display. May contain HTML.
		/// </summary>
		public string complex;
		/// <summary>
		/// Date/time stamp from when this attribute was recorded (or reported) by the device.
		/// </summary>
		public DateTime dts;
		/// <summary>
		/// When false, indicates that this attribute is used by an internal system and should be left untouched.
		/// </summary>
		public bool global;
		/// <summary>
		/// Display name of the attribute.
		/// </summary>
		/// <remarks>max-length: 100</remarks>
		public string name;
		/// <summary>
		/// The device which provided this attribute.
		/// </summary>
		public string provider;
		/// <summary>
		/// Raw value like 3.76 (volts) or true (on).
		/// </summary>
		public object raw;
		/// <summary>
		/// Computed/contextual value from the behaviour. Like "3.76 volts" or "on".
		/// </summary>
		public string simple;
		/// <summary>
		/// Text representation of unit like "°C" or "Km".
		/// </summary>
		public string unit;
	}

	/// <summary> 
	/// Relevant details of an asset which was deleted.
	/// </summary>
	public partial class AssetDeleted {
		/// <summary>
		/// The company to which this asset belonged.
		/// </summary>
		public ulong company;
		/// <summary>
		/// Unique identifier of this asset.
		/// </summary>
		public ulong id;
	}
	/// <summary> 
	/// Seldom changing details about a thing.
	/// </summary>
	public partial class AssetGeneral {
		/// <summary>
		/// The company to which this asset belongs.
		/// </summary>
		public ulong company;
		/// <summary>
		/// The icon that represents this asset on the map and in lists.
		/// </summary>
		public ulong icon;
		/// <summary>
		/// Unique identifier of this asset.
		/// </summary>
		public ulong id;
		/// <summary>
		/// Type of asset.
		/// </summary>
		public AssetType kind;
		/// <summary>
		/// Codified label names.
		/// </summary>
		public string[] labels;
		/// <summary>
		/// The fall-back address which is used to send Messages if the asset is a Person and has no Contact phone or email.
		/// </summary>
		/// <remarks>max-length: 254</remarks>
		public string messagingAddress;
		/// <summary>
		/// This thing's name.
		/// </summary>
		/// <remarks>max-length: 100</remarks>
		public string name;
		/// <summary>
		/// Notes about it.
		/// </summary>
		public string notes;
		/// <summary>
		/// A list of photos of this thing.
		/// </summary>
		public ulong[] pictures;
		/// <summary>
		/// A custom field used to refer to an external system.
		/// </summary>
		/// <remarks>max-length: 100</remarks>
		public string reference;
		/// <summary>
		/// Object version keys used to validate synchronization for all object properties.
		/// </summary>
		public uint[] v;
	}

	/// <summary> 
	/// A simple status for each place an Asset visits.
	/// </summary>
	public partial class AssetPlaceStatus {
		/// <summary>
		/// The date/time stamp for when the Asset first began interacting with the Place.
		/// </summary>
		public DateTime enter;
		/// <summary>
		/// The kind of interaction.
		/// </summary>
		public AssetPlaceStatusType kind;
		/// <summary>
		/// The most recent date/time stamp for the interaction.
		/// </summary>
		public DateTime latest;
	}

	/// <summary> 
	/// Seldom changing details about a person.
	/// </summary>
	public partial class PersonGeneral {
		/// <summary>
		/// The company to which this asset belongs.
		/// </summary>
		public ulong company;
		/// <summary>
		/// A reference to their Company's Contact information.
		/// </summary>
		public ulong contact;
		/// <summary>
		/// The icon that represents this asset on the map and in lists.
		/// </summary>
		public ulong icon;
		/// <summary>
		/// Unique identifier of this asset.
		/// </summary>
		public ulong id;
		/// <summary>
		/// Type of asset.
		/// </summary>
		public AssetType kind;
		/// <summary>
		/// Codified label names.
		/// </summary>
		public string[] labels;
		/// <summary>
		/// The fall-back address which is used to send Messages if the asset is a Person and has no Contact phone or email.
		/// </summary>
		/// <remarks>max-length: 254</remarks>
		public string messagingAddress;
		/// <summary>
		/// This thing's name.
		/// </summary>
		/// <remarks>max-length: 100</remarks>
		public string name;
		/// <summary>
		/// Notes about it.
		/// </summary>
		public string notes;
		/// <summary>
		/// A list of photos of this thing.
		/// </summary>
		public ulong[] pictures;
		/// <summary>
		/// A custom field used to refer to an external system.
		/// </summary>
		/// <remarks>max-length: 100</remarks>
		public string reference;
		/// <summary>
		/// Object version keys used to validate synchronization for all object properties.
		/// </summary>
		public uint[] v;
	}

	/// <summary> 
	/// GPS position information
	/// </summary>
	public partial class Position {
		/// <summary>
		/// Threshold in meters for the accuracy of a position
		/// </summary>
		public uint? accuracy;
		/// <summary>
		/// The road segment description
		/// </summary>
		public string address;
		/// <summary>
		/// Distance in meters from the sea level
		/// </summary>
		public double? altitude;
		/// <summary>
		/// Direction of travel
		/// </summary>
		public ushort? bearing;
		/// <summary>
		/// The Date/Time of the GPS reading
		/// </summary>
		public DateTime dts;
		/// <summary>
		/// Latitude
		/// </summary>
		public double? lat;
		/// <summary>
		/// Longitude
		/// </summary>
		public double? lng;
		/// <summary>
		/// Provider Identifier
		/// </summary>
		public string origin;
		/// <summary>
		/// Speed
		/// </summary>
		public double? speed;
		/// <summary>
		/// The posted speed limit for the road segment
		/// </summary>
		public double? speedLimit;
		/// <summary>
		/// A better description of the current road-segment
		/// </summary>
		public StreetAddress streetAddress;
	}

	/// <summary> 
	/// A road segment description
	/// </summary>
	public partial class StreetAddress {
		/// <summary>
		/// City name
		/// </summary>
		public string city;
		/// <summary>
		/// Country code
		/// </summary>
		public string country;
		/// <summary>
		/// Indicates that there is a toll for the current road segment
		/// </summary>
		public bool isToll;
		/// <summary>
		/// House number
		/// </summary>
		public string number;
		/// <summary>
		/// Postal or zip code
		/// </summary>
		public string postal;
		/// <summary>
		/// Province or state code
		/// </summary>
		public string province;
		/// <summary>
		/// Region name
		/// </summary>
		public string region;
		/// <summary>
		/// Full street name
		/// </summary>
		public string street;
	}

	/// <summary> 
	/// Seldom changing details about a trailer.
	/// </summary>
	public partial class TrailerGeneral {
		/// <summary>
		/// Primary colour of the trailer (given in 24bit hex; #RRGGBB)
		/// </summary>
		/// <remarks>max-length: 22</remarks>
		public string colour;
		/// <summary>
		/// The company to which this asset belongs.
		/// </summary>
		public ulong company;
		/// <summary>
		/// The icon that represents this asset on the map and in lists.
		/// </summary>
		public ulong icon;
		/// <summary>
		/// Unique identifier of this asset.
		/// </summary>
		public ulong id;
		/// <summary>
		/// Type of asset.
		/// </summary>
		public AssetType kind;
		/// <summary>
		/// Codified label names.
		/// </summary>
		public string[] labels;
		/// <summary>
		/// Manufacturer's name.
		/// </summary>
		/// <remarks>max-length: 50</remarks>
		public string make;
		/// <summary>
		/// The fall-back address which is used to send Messages if the asset is a Person and has no Contact phone or email.
		/// </summary>
		/// <remarks>max-length: 254</remarks>
		public string messagingAddress;
		/// <summary>
		/// Manufacturer's model name/number.
		/// </summary>
		/// <remarks>max-length: 50</remarks>
		public string model;
		/// <summary>
		/// This thing's name.
		/// </summary>
		/// <remarks>max-length: 100</remarks>
		public string name;
		/// <summary>
		/// Notes about it.
		/// </summary>
		public string notes;
		/// <summary>
		/// A list of photos of this thing.
		/// </summary>
		public ulong[] pictures;
		/// <summary>
		/// The license plate.
		/// </summary>
		/// <remarks>max-length: 50</remarks>
		public string plate;
		/// <summary>
		/// A custom field used to refer to an external system.
		/// </summary>
		/// <remarks>max-length: 100</remarks>
		public string reference;
		/// <summary>
		/// Manufacturer's unique identification number for this trailer.
		/// </summary>
		/// <remarks>max-length: 50</remarks>
		public string serial;
		/// <summary>
		/// Object version keys used to validate synchronization for all object properties.
		/// </summary>
		public uint[] v;
		/// <summary>
		/// Year of manufacturing.
		/// </summary>
		public ushort year;
	}

	/// <summary> 
	/// Often changing details about a vehicle.
	/// </summary>
	public partial class VehicleAdvanced {
		/// <summary>
		/// A list of attributes given to this asset by the connection device such as wiring state, VBus, etc.
		/// The object's keys follow the codified format.
		/// </summary>
		/// <seealso cref="codified" />
		public Dictionary<codified, AssetAttribute> attributes;
		/// <summary>
		/// The company to which this asset belongs.
		/// </summary>
		public ulong company;
		/// <summary>
		/// The cumulative duration that the vehicle's engine has been running (in decimal hours).
		/// </summary>
		public double engineHours;
		/// <summary>
		/// Unique identifier of this asset.
		/// </summary>
		public ulong id;
		/// <summary>
		/// The cumulative distance travelled in kilometres.
		/// </summary>
		public double odometer;
		/// <summary>
		/// The current state of this asset's interaction with known Places.
		/// The object's keys follow the uint64 format.
		/// </summary>
		/// <seealso cref="uint64" />
		public Dictionary<ulong, AssetPlaceStatus> places;
		/// <summary>
		/// The things GPS coordinates including speed, bearing, and street information.
		/// </summary>
		public Position position;
		/// <summary>
		/// The list of devices providing events for this asset.
		/// </summary>
		public string[] providers;
		/// <summary>
		/// A list of assets related to this one; like a Person for a Vehicle (driver).
		/// </summary>
		public ulong[] relationships;
		/// <summary>
		/// The codified status tag names.
		/// </summary>
		public string[] tags;
		/// <summary>
		/// Object version keys used to validate synchronization for all object properties.
		/// </summary>
		public uint[] v;
	}

	/// <summary> 
	/// Seldom changing details about a vehicle.
	/// </summary>
	public partial class VehicleGeneral {
		/// <summary>
		/// Primary colour of the trailer (given in 24bit hex; #RRGGBB)
		/// </summary>
		/// <remarks>max-length: 22</remarks>
		public string colour;
		/// <summary>
		/// The company to which this asset belongs.
		/// </summary>
		public ulong company;
		/// <summary>
		/// The icon that represents this asset on the map and in lists.
		/// </summary>
		public ulong icon;
		/// <summary>
		/// Unique identifier of this asset.
		/// </summary>
		public ulong id;
		/// <summary>
		/// Type of asset.
		/// </summary>
		public AssetType kind;
		/// <summary>
		/// Codified label names.
		/// </summary>
		public string[] labels;
		/// <summary>
		/// Manufacturer's name.
		/// </summary>
		/// <remarks>max-length: 50</remarks>
		public string make;
		/// <summary>
		/// The fall-back address which is used to send Messages if the asset is a Person and has no Contact phone or email.
		/// </summary>
		/// <remarks>max-length: 254</remarks>
		public string messagingAddress;
		/// <summary>
		/// Manufacturer's model name/number.
		/// </summary>
		/// <remarks>max-length: 50</remarks>
		public string model;
		/// <summary>
		/// This thing's name.
		/// </summary>
		/// <remarks>max-length: 100</remarks>
		public string name;
		/// <summary>
		/// Notes about it.
		/// </summary>
		public string notes;
		/// <summary>
		/// A list of photos of this thing.
		/// </summary>
		public ulong[] pictures;
		/// <summary>
		/// The license plate.
		/// </summary>
		/// <remarks>max-length: 50</remarks>
		public string plate;
		/// <summary>
		/// A custom field used to refer to an external system.
		/// </summary>
		/// <remarks>max-length: 100</remarks>
		public string reference;
		/// <summary>
		/// Object version keys used to validate synchronization for all object properties.
		/// </summary>
		public uint[] v;
		/// <summary>
		/// Manufacturer's unique identification number (Vehicle Identification Number).
		/// </summary>
		/// <remarks>max-length: 50</remarks>
		public string vin;
		/// <summary>
		/// Year of manufacturing.
		/// </summary>
		public ushort year;
	}

	#endregion Assets
	#region Behaviours
	/// <summary>
	/// The type of logged message.
	/// </summary>
	public enum BehaviourLogType {
		/// <summary> 
		/// Used for errors.
		/// </summary>
		err,
		/// <summary> 
		/// Used for more important messages.
		/// </summary>
		info,
		/// <summary> 
		/// Used for general information messages.
		/// </summary>
		log,
		/// <summary> 
		/// Used for warnings.
		/// </summary>
		warn
	}

	/// <summary>
	/// Possible data-types given to BehaviourParameters.
	/// </summary>
	public enum BehaviourParameterType {
		/// <summary> 
		/// True or false.
		/// </summary>
		boolean,
		/// <summary> 
		/// Object or array literal.
		/// </summary>
		json,
		/// <summary> 
		/// Numeric value (signed double-precision floating point number).
		/// </summary>
		number,
		/// <summary> 
		/// Text.
		/// </summary>
		@string
	}

	/// <summary> 
	/// The applied behaviour which includes all parameters and targets specific assets.
	/// </summary>
	public partial class Behaviour {
		/// <summary>
		/// The company to which this behaviour belongs.
		/// </summary>
		public ulong company;
		/// <summary>
		/// A search pattern used to filter the providers which can implement this behaviour.
		/// </summary>
		public expression filters;
		/// <summary>
		/// Unique identifier of this behaviour.
		/// </summary>
		public ulong id;
		/// <summary>
		/// The name of this behaviour.
		/// </summary>
		/// <remarks>max-length: 100</remarks>
		public string name;
		/// <summary>
		/// Notes.
		/// </summary>
		public string notes;
		/// <summary>
		/// The list of defined variable name/value pairs for the script requires.
		/// </summary>
		public Dictionary<string, BehaviourParameter> parameters;
		/// <summary>
		/// The priority flag allows you to define an execution order for all behaviours for a provider.
		/// </summary>
		public byte priority;
		/// <summary>
		/// The script which this behaviour implements.
		/// </summary>
		public ulong script;
		/// <summary>
		/// The search pattern used to target the assets which will embed this behaviour in their execution context.
		/// </summary>
		public expression targets;
		/// <summary>
		/// Object version keys used to validate synchronization for all object properties.
		/// </summary>
		public uint[] v;
	}

	/// <summary> 
	/// Relevant details of a behaviour which was deleted.
	/// </summary>
	public partial class BehaviourDeleted {
		/// <summary>
		/// The company to which this behaviour belonged.
		/// </summary>
		public ulong company;
		/// <summary>
		/// Unique identifier of this behaviour.
		/// </summary>
		public ulong id;
		/// <summary>
		/// The script this behaviour implemented.
		/// </summary>
		public ulong script;
	}

	/// <summary> 
	/// A debug message available to script writers to help debug and trace output from a BehaviourScript.
	/// </summary>
	public partial class BehaviourLog {
		/// <summary>
		/// The asset which whose behaviours created this log entry.
		/// </summary>
		public ulong asset;
		/// <summary>
		/// The behaviour to which this log message belongs.
		/// </summary>
		public ulong? behaviour;
		/// <summary>
		/// The character column where the error was generated.
		/// </summary>
		public uint? character;
		/// <summary>
		/// The company to which this log message belongs.
		/// </summary>
		public ulong company;
		/// <summary>
		/// When the log entry was generated by the script.
		/// </summary>
		public DateTime dts;
		/// <summary>
		/// Unique identifier of this log message.
		/// </summary>
		public ulong id;
		/// <summary>
		/// The category of message.
		/// </summary>
		public BehaviourLogType kind;
		/// <summary>
		/// The line number of the script which generated this message.
		/// </summary>
		public uint line;
		/// <summary>
		/// The body of the message.
		/// </summary>
		public string message;
		/// <summary>
		/// The script which generated this log message.
		/// </summary>
		public ulong script;
		/// <summary>
		/// Object version keys used to validate synchronization for all object properties.
		/// </summary>
		public uint[] v;
	}

	/// <summary> 
	/// Relevant details of a log message which was deleted.
	/// </summary>
	public partial class BehaviourLogDeleted {
		/// <summary>
		/// The behaviour to which this log message belonged.
		/// </summary>
		public ulong behaviour;
		/// <summary>
		/// The company to which the behaviour belonged.
		/// </summary>
		public ulong company;
		/// <summary>
		/// Unique identifier of this log message.
		/// </summary>
		public ulong id;
		/// <summary>
		/// The script to which this log message belonged.
		/// </summary>
		public ulong script;
	}
	/// <summary> 
	/// Definition of an argument passed to a Behaviour Script.
	/// </summary>
	public partial class BehaviourParameter {
		/// <summary>
		/// Gives a hint to the client on the best UI to use for editing. For example, "checkbox" is a good UI hint for boolean parameter types.
		/// </summary>
		public string context;
		/// <summary>
		/// Usage notes.
		/// </summary>
		public string notes;
		/// <summary>
		/// Simple type information for the compiler.
		/// </summary>
		public BehaviourParameterType type;
		/// <summary>
		/// The value is given as a string, but parsed into native type when compiled.
		/// </summary>
		public string value;
	}

	/// <summary> 
	/// Business logic run by the system to react to GPS events and device information.
	/// </summary>
	public partial class BehaviourScript {
		/// <summary>
		/// The company to which this script belongs.
		/// </summary>
		public ulong company;
		/// <summary>
		/// Flag set by the compiler if this code compiles
		/// </summary>
		public bool compiles;
		/// <summary>
		/// The background colour given to this script for easy visual identification (given in 24bit hex; #RRGGBB)
		/// </summary>
		/// <remarks>max-length: 22</remarks>
		public string fill;
		/// <summary>
		/// A list of targeting expressions. These expressions are defaults for derived Behaviours.
		/// </summary>
		public expression filters;
		/// <summary>
		/// Indicates whether this script is available to child companies.
		/// </summary>
		public bool global;
		/// <summary>
		/// The codified graphic name given to this script for easy visual identification.
		/// </summary>
		/// <remarks>max-length: 22</remarks>
		public codified graphic;
		/// <summary>
		/// Unique identifier of this script.
		/// </summary>
		public ulong id;
		/// <summary>
		/// The nickname given to this script.
		/// </summary>
		/// <remarks>max-length: 100</remarks>
		public string name;
		/// <summary>
		/// Usage notes and instructions for users on how best to setup this script.
		/// </summary>
		public string notes;
		/// <summary>
		/// Listed parameters for the Behaviour function.
		/// </summary>
		public Dictionary<string, BehaviourParameter> parameters;
		/// <summary>
		/// The source code.
		/// </summary>
		/// <remarks>max-length: 8060</remarks>
		public string source;
		/// <summary>
		/// The text/graphic colour given to this script for easy visual identification (given in 24bit hex; #RRGGBB)
		/// </summary>
		/// <remarks>max-length: 22</remarks>
		public string stroke;
		/// <summary>
		/// Object version keys used to validate synchronization for all object properties.
		/// </summary>
		public uint[] v;
	}

	/// <summary> 
	/// Relevant details of a script which was deleted.
	/// </summary>
	public partial class BehaviourScriptDeleted {
		/// <summary>
		/// The company to which this script belonged.
		/// </summary>
		public ulong company;
		/// <summary>
		/// Unique identifier of this script.
		/// </summary>
		public ulong id;
	}

	#endregion Behaviours
	#region Billing
	/// <summary>
	/// The kind of service being billed.
	/// </summary>
	public enum BillableHostingType {
		/// <summary> 
		/// Generic dot-on-a-map tracking
		/// </summary>
		asset,
		/// <summary> 
		/// Tasks assignable to vehicles or persons
		/// </summary>
		dispatch,
		/// <summary> 
		/// FMCSA compliant E-Logs and Hours of Service
		/// </summary>
		elogs,
		/// <summary> 
		/// Employee/driver tracking
		/// </summary>
		mobile,
		/// <summary> 
		/// Dashcam and live images hosting
		/// </summary>
		streetview,
		/// <summary> 
		/// Vehicle tracking (includes VBus data, and engine hours)
		/// </summary>
		vehicle
	}

	/// <summary>
	/// The kind of license being billed.
	/// </summary>
	public enum BillableLicenseType {
		/// <summary> 
		/// SmartWitness data hosting fee
		/// </summary>
		smartwitness
	}

	/// <summary>
	/// Used for invoices.
	/// </summary>
	public enum BillingCurrency {
		/// <summary> 
		/// Canadian dollars
		/// </summary>
		CAD,
		/// <summary> 
		/// Eurozone currency
		/// </summary>
		EURO,
		/// <summary> 
		/// American dollars
		/// </summary>
		USD
	}

	/// <summary>
	/// The type of repeating cycle used for generating bills.
	/// </summary>
	public enum BillingCycle {
		/// <summary> 
		/// Once per year
		/// </summary>
		annually,
		/// <summary> 
		/// Every day
		/// </summary>
		daily,
		/// <summary> 
		/// Once a month
		/// </summary>
		monthly,
		/// <summary> 
		/// Every three months
		/// </summary>
		quarterly,
		/// <summary> 
		/// Each week
		/// </summary>
		weekly
	}

	/// <summary> 
	/// A discount rule for assets
	/// </summary>
	public partial class BillableHostingDiscount {
		/// <summary>
		/// Cost per Asset
		/// </summary>
		public double amount;
		/// <summary>
		/// Date this billing rule is applied until; null means it never ends. These dates are used to determine how much of the cycle is billed.
		/// </summary>
		public DateTime? end;
		/// <summary>
		/// Unique identifier of this hosting rule.
		/// </summary>
		public ulong id;
		/// <summary>
		/// The number of units to which this billing rule applies.
		/// </summary>
		public int limit;
		/// <summary>
		/// The name of this billing rule.
		/// </summary>
		/// <remarks>max-length: 100</remarks>
		public string name;
		/// <summary>
		/// Notes about billing this rule.
		/// </summary>
		public string notes;
		/// <summary>
		/// When true, the amount is used as a percentage value instead of a currency values.
		/// </summary>
		public bool percentage;
		/// <summary>
		/// Unique identifier of this rule's billing profile.
		/// </summary>
		public ulong profile;
		/// <summary>
		/// A custom field used to refer to an external system. Examples are a cost codes, SOCs, discount plans...
		/// </summary>
		/// <remarks>max-length: 100</remarks>
		public string reference;
		/// <summary>
		/// The type of services being discounted.
		/// </summary>
		public BillableHostingType[] services;
		/// <summary>
		/// Date this billing rule takes effect. These dates are used to determine how much of the cycle is billed.
		/// </summary>
		public DateTime start;
		/// <summary>
		/// Does this hosting rule apply to suspended resources
		/// </summary>
		public bool suspended;
		/// <summary>
		/// Which assets are targetted by this hosting rule
		/// </summary>
		public expression targets;
		/// <summary>
		/// Object version keys used to validate synchronization for all object properties.
		/// </summary>
		public uint[] v;
	}

	/// <summary> 
	/// A billing rule for assets
	/// </summary>
	public partial class BillableHostingRule {
		/// <summary>
		/// Cost per Asset
		/// </summary>
		public double amount;
		/// <summary>
		/// Date this billing rule is applied until; null means it never ends. These dates are used to determine how much of the cycle is billed.
		/// </summary>
		public DateTime? end;
		/// <summary>
		/// Unique identifier of this hosting rule.
		/// </summary>
		public ulong id;
		/// <summary>
		/// The number of units to which this billing rule applies.
		/// </summary>
		public int limit;
		/// <summary>
		/// The name of this billing rule.
		/// </summary>
		/// <remarks>max-length: 100</remarks>
		public string name;
		/// <summary>
		/// Notes about billing this rule.
		/// </summary>
		public string notes;
		/// <summary>
		/// Unique identifier of this rule's billing profile.
		/// </summary>
		public ulong profile;
		/// <summary>
		/// A custom field used to refer to an external system. Examples are a cost codes, SOCs, discount plans...
		/// </summary>
		/// <remarks>max-length: 100</remarks>
		public string reference;
		/// <summary>
		/// The type of service being billed.
		/// </summary>
		public BillableHostingType service;
		/// <summary>
		/// Date this billing rule takes effect. These dates are used to determine how much of the cycle is billed.
		/// </summary>
		public DateTime start;
		/// <summary>
		/// Does this hosting rule apply to suspended resources
		/// </summary>
		public bool suspended;
		/// <summary>
		/// Which assets are targetted by this hosting rule
		/// </summary>
		public expression targets;
		/// <summary>
		/// Object version keys used to validate synchronization for all object properties.
		/// </summary>
		public uint[] v;
	}

	/// <summary> 
	/// A hardware license for providers
	/// </summary>
	public partial class BillableLicense {
		/// <summary>
		/// Cost per Asset
		/// </summary>
		public double amount;
		/// <summary>
		/// Date this billing rule is applied until; null means it never ends. These dates are used to determine how much of the cycle is billed.
		/// </summary>
		public DateTime? end;
		/// <summary>
		/// Unique identifier of this hosting rule.
		/// </summary>
		public ulong id;
		/// <summary>
		/// The type of hardware license
		/// </summary>
		public BillableLicenseType kind;
		/// <summary>
		/// The number of units to which this billing rule applies.
		/// </summary>
		public int limit;
		/// <summary>
		/// The name of this billing rule.
		/// </summary>
		/// <remarks>max-length: 100</remarks>
		public string name;
		/// <summary>
		/// Notes about billing this rule.
		/// </summary>
		public string notes;
		/// <summary>
		/// Unique identifier of this rule's billing profile.
		/// </summary>
		public ulong profile;
		/// <summary>
		/// A custom field used to refer to an external system. Examples are a cost codes, SOCs, discount plans...
		/// </summary>
		/// <remarks>max-length: 100</remarks>
		public string reference;
		/// <summary>
		/// Date this billing rule takes effect. These dates are used to determine how much of the cycle is billed.
		/// </summary>
		public DateTime start;
		/// <summary>
		/// Does this hosting rule apply to suspended resources
		/// </summary>
		public bool suspended;
		/// <summary>
		/// Which assets are targetted by this hosting rule
		/// </summary>
		public expression targets;
		/// <summary>
		/// Object version keys used to validate synchronization for all object properties.
		/// </summary>
		public uint[] v;
	}

	/// <summary> 
	/// A cellular/data rate plan
	/// </summary>
	public partial class BillableRatePlan {
		/// <summary>
		/// Cost per cycle for this plan
		/// </summary>
		public double amount;
		/// <summary>
		/// Cost per kilobytes over the included volume
		/// </summary>
		public double amountOverageKB;
		/// <summary>
		/// Cost per message over the included volume
		/// </summary>
		public double amountOverageSMS;
		/// <summary>
		/// Date this billing rule is applied until; null means it never ends. These dates are used to determine how much of the cycle is billed.
		/// </summary>
		public DateTime? end;
		/// <summary>
		/// Unique identifier of this hosting rule.
		/// </summary>
		public ulong id;
		/// <summary>
		/// The name of this billing rule.
		/// </summary>
		/// <remarks>max-length: 100</remarks>
		public string name;
		/// <summary>
		/// Notes about billing this rule.
		/// </summary>
		public string notes;
		/// <summary>
		/// Unique identifier of this rule's billing profile.
		/// </summary>
		public ulong profile;
		/// <summary>
		/// A custom field used to refer to an external system. Examples are a cost codes, SOCs, discount plans...
		/// </summary>
		/// <remarks>max-length: 100</remarks>
		public string reference;
		/// <summary>
		/// Date this billing rule takes effect. These dates are used to determine how much of the cycle is billed.
		/// </summary>
		public DateTime start;
		/// <summary>
		/// Object version keys used to validate synchronization for all object properties.
		/// </summary>
		public uint[] v;
		/// <summary>
		/// The volume of data (in kilobytes) included with this plan.
		/// </summary>
		public int volumeKB;
		/// <summary>
		/// The number of SMS messages included with the plan
		/// </summary>
		public int volumeSMS;
	}

	/// <summary> 
	/// White-labeled service details
	/// </summary>
	public partial class BillableReseller {
		/// <summary>
		/// Cost per cycle for this plan
		/// </summary>
		public double amount;
		/// <summary>
		/// Is this reseller allowed to use the billing system
		/// </summary>
		public bool billingSystem;
		/// <summary>
		/// Date this billing rule is applied until; null means it never ends. These dates are used to determine how much of the cycle is billed.
		/// </summary>
		public DateTime? end;
		/// <summary>
		/// Unique identifier of this hosting rule.
		/// </summary>
		public ulong id;
		/// <summary>
		/// Number of websites hosted
		/// </summary>
		public int instances;
		/// <summary>
		/// The name of this billing rule.
		/// </summary>
		/// <remarks>max-length: 100</remarks>
		public string name;
		/// <summary>
		/// Notes about billing this rule.
		/// </summary>
		public string notes;
		/// <summary>
		/// Unique identifier of this rule's billing profile.
		/// </summary>
		public ulong profile;
		/// <summary>
		/// A custom field used to refer to an external system. Examples are a cost codes, SOCs, discount plans...
		/// </summary>
		/// <remarks>max-length: 100</remarks>
		public string reference;
		/// <summary>
		/// Date this billing rule takes effect. These dates are used to determine how much of the cycle is billed.
		/// </summary>
		public DateTime start;
		/// <summary>
		/// Object version keys used to validate synchronization for all object properties.
		/// </summary>
		public uint[] v;
		/// <summary>
		/// Number of virtual numbers used to send/receive SMS messages
		/// </summary>
		public int virtualSMS;
	}
	/// <summary> 
	/// Description of a tiered SMS messaging limit
	/// </summary>
	public partial class BillableSmsProfile {
		/// <summary>
		/// Cost per SMS message sent. Received messages are free.
		/// </summary>
		public double amount;
		/// <summary>
		/// The maximum number of messages sent per cycle
		/// </summary>
		public uint limit;
	}

	/// <summary> 
	/// Support contract details
	/// </summary>
	public partial class BillableSupport {
		/// <summary>
		/// Cost per cycle for this plan
		/// </summary>
		public double amount;
		/// <summary>
		/// Cost (per hour) over the included amount
		/// </summary>
		public double amountOverage;
		/// <summary>
		/// Date this billing rule is applied until; null means it never ends. These dates are used to determine how much of the cycle is billed.
		/// </summary>
		public DateTime? end;
		/// <summary>
		/// Unique identifier of this hosting rule.
		/// </summary>
		public ulong id;
		/// <summary>
		/// Amount of included time for this customer/technical support package
		/// </summary>
		public TimeSpan included;
		/// <summary>
		/// The name of this billing rule.
		/// </summary>
		/// <remarks>max-length: 100</remarks>
		public string name;
		/// <summary>
		/// Notes about billing this rule.
		/// </summary>
		public string notes;
		/// <summary>
		/// Unique identifier of this rule's billing profile.
		/// </summary>
		public ulong profile;
		/// <summary>
		/// A custom field used to refer to an external system. Examples are a cost codes, SOCs, discount plans...
		/// </summary>
		/// <remarks>max-length: 100</remarks>
		public string reference;
		/// <summary>
		/// Date this billing rule takes effect. These dates are used to determine how much of the cycle is billed.
		/// </summary>
		public DateTime start;
		/// <summary>
		/// Object version keys used to validate synchronization for all object properties.
		/// </summary>
		public uint[] v;
	}

	/// <summary> 
	/// A profile used to generate billable orders for a customer.
	/// </summary>
	public partial class CompanyBillingProfile {
		/// <summary>
		/// Unique identifier of the Company receiving the bill. Most of the time, this value is the same as the company.
		/// </summary>
		public ulong billee;
		/// <summary>
		/// Unique identifier of the Company to which this rule pertains.
		/// </summary>
		public ulong company;
		/// <summary>
		/// kind of money
		/// </summary>
		public BillingCurrency currency;
		/// <summary>
		/// Repeating cycle used for generating bills
		/// </summary>
		public BillingCycle cycle;
		/// <summary>
		/// When should the cycle end (customer cancelled)
		/// </summary>
		public DateTime? cycleEnd;
		/// <summary>
		/// Pro-rated, or post-dated.
		/// </summary>
		public bool cyclePostDated;
		/// <summary>
		/// When is the first day of the first cycle
		/// </summary>
		public DateTime cycleStart;
		/// <summary>
		/// Lists of bundle discounts used to generate bills.
		/// </summary>
		public BillableHostingRule[] discounts;
		/// <summary>
		/// Are the Google services available to be proxied by the service?
		/// </summary>
		public bool googleServicesEnabled;
		/// <summary>
		/// Unique identifier of this billing profile
		/// </summary>
		public ulong id;
		/// <summary>
		/// List of hardware licenses
		/// </summary>
		public BillableLicense[] licenses;
		/// <summary>
		/// SMS messaging tiers
		/// </summary>
		public BillableSmsProfile[] messages;
		/// <summary>
		/// The name for this profile
		/// </summary>
		/// <remarks>max-length: 100</remarks>
		public string name;
		/// <summary>
		/// Notes about the billing profile for the billee or target.
		/// </summary>
		/// <remarks>max-length: 1000</remarks>
		public string notes;
		/// <summary>
		/// List of cellular/data rate plans
		/// </summary>
		public BillableRatePlan[] plans;
		/// <summary>
		/// Unique identifier of the Company that owns this profile and is sending the bill.
		/// </summary>
		public ulong reseller;
		/// <summary>
		/// Lists of billing rules used to generate bills.
		/// </summary>
		public BillableHostingRule[] rules;
		/// <summary>
		/// List of customer and technical support contracts
		/// </summary>
		public BillableSupport[] support;
		/// <summary>
		/// Object version keys used to validate synchronization for all object properties.
		/// </summary>
		public uint[] v;
		/// <summary>
		/// Details about a white-labeled solution
		/// </summary>
		public BillableReseller whitelabel;
	}

	#endregion Billing
	#region Companies
	/// <summary>
	/// Defines how User passwords expire.
	/// </summary>
	public enum ExpiryModePolicy {
		/// <summary> 
		/// Passwords expire after a defined number of days.
		/// </summary>
		days,
		/// <summary> 
		/// Passwords never expire.
		/// </summary>
		never,
		/// <summary> 
		/// Passwords expire after a defined number of successful logins.
		/// </summary>
		sessions
	}

	/// <summary>
	/// Defines the behaviour of the system when a user creates multiple sessions.
	/// </summary>
	public enum MultiUserPolicy {
		/// <summary> 
		/// Allow users to create multiple simultaneous sessions.
		/// </summary>
		allow,
		/// <summary> 
		/// Deny users from creating new sessions if they already have an active session.
		/// </summary>
		deny,
		/// <summary> 
		/// Allow users to create a new session, but automatically kill the previous session.
		/// </summary>
		replace
	}

	/// <summary> 
	/// The organization to which everything else belongs. All other objects are either a child of a company, or a child of an object which belongs to a company.
	/// </summary>
	public partial class Company {
		/// <summary>
		/// Unique identifier of the Company.
		/// </summary>
		public ulong id;
		/// <summary>
		/// The styles for labels added to Assets, Places, and other things.
		/// </summary>
		public LabelStyle[] labels;
		/// <summary>
		/// The organizational name
		/// </summary>
		/// <remarks>max-length: 100</remarks>
		public string name;
		/// <summary>
		/// Notes.
		/// </summary>
		public string notes;
		/// <summary>
		/// The unique identifier of this company's parent organization.
		/// </summary>
		public ulong parent;
		/// <summary>
		/// The password complexity and expiry policy.
		/// </summary>
		public PasswordPolicy passwordPolicy;
		/// <summary>
		/// The session lifetime policy.
		/// </summary>
		public SessionPolicy sessionPolicy;
		/// <summary>
		/// The styles for status tags added to Assets.
		/// </summary>
		public LabelStyle[] tags;
		/// <summary>
		/// List of groups available for users of this company.
		/// </summary>
		public UserGroup[] userGroups;
		/// <summary>
		/// Object version keys used to validate synchronization for different properties.
		/// v[0]: Properties found in the CompanyGeneral object.
		/// v[1]: Is not yet in use.
		/// v[2]: Is not yet in use.
		/// v[3]: Properties found in the CompanyLabels object.
		/// v[4]: Properties found in the CompanyPolicies object.
		/// v[5]: Properties found in the CompanyReseller object.
		/// </summary>
		/// <remarks>length: 6</remarks>
		public uint[] v;
	}

	/// <summary> 
	/// Relevant details of a company which was deleted.
	/// </summary>
	public partial class CompanyDeleted {
		/// <summary>
		/// Unique identifier of this company.
		/// </summary>
		public ulong id;
		/// <summary>
		/// Unique identifier of this parent-organization's company.
		/// </summary>
		public ulong parent;
	}
	/// <summary> 
	/// General details about a company.
	/// </summary>
	public partial class CompanyGeneral {
		/// <summary>
		/// Unique identifier of the Company.
		/// </summary>
		public ulong id;
		/// <summary>
		/// The organizational name
		/// </summary>
		/// <remarks>max-length: 100</remarks>
		public string name;
		/// <summary>
		/// Notes.
		/// </summary>
		public string notes;
		/// <summary>
		/// The unique identifier of this company's parent organization.
		/// </summary>
		public ulong parent;
		/// <summary>
		/// Object version keys used to validate synchronization for all object properties.
		/// </summary>
		public uint[] v;
	}

	/// <summary> 
	/// The colours and styles used by this company to tag and label Assets, Places, and other things.
	/// </summary>
	public partial class CompanyLabels {
		/// <summary>
		/// Unique identifier of the Company.
		/// </summary>
		public ulong id;
		/// <summary>
		/// The styles for labels added to Assets, Places, and other things.
		/// </summary>
		public LabelStyle[] labels;
		/// <summary>
		/// The unique identifier of this company's parent organization.
		/// </summary>
		public ulong parent;
		/// <summary>
		/// The styles for status tags added to Assets.
		/// </summary>
		public LabelStyle[] tags;
		/// <summary>
		/// Object version keys used to validate synchronization for all object properties.
		/// </summary>
		public uint[] v;
	}

	/// <summary> 
	/// The password and session lifetime policies for this Company.
	/// </summary>
	public partial class CompanyPolicies {
		/// <summary>
		/// Unique identifier of the Company.
		/// </summary>
		public ulong id;
		/// <summary>
		/// The unique identifier of this company's parent organization.
		/// </summary>
		public ulong parent;
		/// <summary>
		/// The password complexity and expiry policy.
		/// </summary>
		public PasswordPolicy passwordPolicy;
		/// <summary>
		/// The session lifetime policy.
		/// </summary>
		public SessionPolicy sessionPolicy;
		/// <summary>
		/// Object version keys used to validate synchronization for all object properties.
		/// </summary>
		public uint[] v;
	}

	/// <summary> 
	/// Visual style identification helper.
	/// </summary>
	public partial class LabelStyle {
		/// <summary>
		/// The codified name of this style
		/// </summary>
		public codified code;
		/// <summary>
		/// The background colour given to this style for easy visual identification (given in 24bit hex; #RRGGBB)
		/// </summary>
		/// <remarks>max-length: 22</remarks>
		public string fill;
		/// <summary>
		/// The codified graphic name given to this script for easy visual identification.
		/// </summary>
		/// <remarks>max-length: 22</remarks>
		public codified graphic;
		/// <summary>
		/// The name of this visual style.
		/// </summary>
		/// <remarks>max-length: 100</remarks>
		public string name;
		/// <summary>
		/// Notes!
		/// </summary>
		public string notes;
		/// <summary>
		/// The old codified name of this style.
		/// </summary>
		public codified old;
		/// <summary>
		/// The text/graphic colour given to this style for easy visual identification (given in 24bit hex; #RRGGBB)
		/// </summary>
		/// <remarks>max-length: 22</remarks>
		public string stroke;
	}

	/// <summary> 
	/// The password complexity and expiry policy.
	/// </summary>
	public partial class PasswordPolicy {
		/// <summary>
		/// Defines how passwords expire.
		/// </summary>
		public ExpiryModePolicy expireMode;
		/// <summary>
		/// The threshold for expiry.
		/// </summary>
		public byte expireThreshold;
		/// <summary>
		/// Do passwords require alphabetical characters.
		/// </summary>
		public bool includeLetters;
		/// <summary>
		/// Do passwords require numeric characters.
		/// </summary>
		public bool includeNumbers;
		/// <summary>
		/// Do passwords require non-alphanumeric characters.
		/// </summary>
		public bool includeSpecial;
		/// <summary>
		/// Do passwords require upper-case and lower-case letters.
		/// </summary>
		public bool includeUpperLower;
		/// <summary>
		/// The minimum number of characters required.
		/// </summary>
		public byte minimumLength;
	}

	/// <summary> 
	/// The session lifetime policy.
	/// </summary>
	public partial class SessionPolicy {
		/// <summary>
		/// The list of applications users are allowed to use to create sessions.
		/// </summary>
		public string[] applications;
		/// <summary>
		/// The lifetime duration of a session in minutes.
		/// </summary>
		public ushort expireTimeout;
		/// <summary>
		/// Defines whether a session should be automatically killed when the connection breaks.
		/// </summary>
		public bool idleAllowed;
		/// <summary>
		/// Restrict session creation to only the provided IPv4 ranges (using CIDR slash-notation). Leave blank for Internet access.
		/// </summary>
		public string[] ipv4Ranges;
		/// <summary>
		/// The maximum number of sessions allowed per user.
		/// </summary>
		public byte maxSessions;
		/// <summary>
		/// Defines the behaviour of the system when a user creates multiple sessions.
		/// </summary>
		public MultiUserPolicy multiUser;
	}

	#endregion Companies
	#region Contacts
	/// <summary> 
	/// Contact information
	/// </summary>
	public partial class Contact {
		/// <summary>
		/// Mailing addresses
		/// Use the object key like a name of the address.
		/// Example keys: Home, Work, Park, etc.
		/// </summary>
		/// <remarks>max-values-length: 254</remarks>
		public Dictionary<string, string> addresses;
		/// <summary>
		/// The company to which this contact belongs
		/// </summary>
		public ulong company;
		/// <summary>
		/// Date information
		/// Use the object key like a name of the date.
		/// Example keys: Birthday, Started Date, Retired On, etc.
		/// </summary>
		public Dictionary<string, string> dates;
		/// <summary>
		/// Email addresses
		/// Use the object key like a name of the address.
		/// Example keys: Home, Work, Support, Old, etc.
		/// </summary>
		/// <remarks>max-values-length: 254</remarks>
		public Dictionary<string, string> emails;
		/// <summary>
		/// Unique identifier of this contact.
		/// </summary>
		public ulong id;
		/// <summary>
		/// The person's name
		/// </summary>
		/// <remarks>max-length: 100</remarks>
		public string name;
		/// <summary>
		/// Notes about this person.
		/// </summary>
		public string notes;
		/// <summary>
		/// Uncategorized information
		/// Use the object keys and values however you'd like.
		/// </summary>
		/// <remarks>max-values-length: 254</remarks>
		public Dictionary<string, string> options;
		/// <summary>
		/// A collection of other names this person might go by.
		/// Use the object key like a name identifier.
		/// Example keys: Initials, Nickname, Maiden Name, etc.
		/// </summary>
		/// <remarks>max-values-length: 254</remarks>
		public Dictionary<string, string> otherNames;
		/// <summary>
		/// Phone numbers.
		/// Use the object key like a name of the phone number.
		/// Example keys: Mobile, Fax, Home, Office, etc.
		/// </summary>
		public Dictionary<string, ulong> phones;
		/// <summary>
		/// Pictures of this Contact.
		/// </summary>
		public ulong[] pictures;
		/// <summary>
		/// A list of roles they play in the Company.
		/// </summary>
		public string[] roles;
		/// <summary>
		/// Websites and other online resources
		/// Use the object key like a name of the address.
		/// Example keys: Downloads, Support, FTP, etc.
		/// </summary>
		/// <remarks>max-values-length: 254</remarks>
		public Dictionary<string, string> urls;
		/// <summary>
		/// Object version keys used to validate synchronization for all object properties.
		/// </summary>
		public uint[] v;
	}

	/// <summary> 
	/// Relevant details of a contact which was deleted.
	/// </summary>
	public partial class ContactDeleted {
		/// <summary>
		/// The company to which this contact belonged.
		/// </summary>
		public ulong company;
		/// <summary>
		/// Unique identifier of this contact.
		/// </summary>
		public ulong id;
	}
	#endregion Contacts
	#region Dispatch Tasks
	/// <summary>
	/// Tasks have a lifetime and each status represents a task's progress through it's life.
	/// </summary>
	public enum TaskStatus {
		/// <summary> 
		/// The asset has arrived at the task's location.
		/// </summary>
		arrived,
		/// <summary> 
		/// The task was cancelled by either the asset or a user.
		/// </summary>
		cancelled,
		/// <summary> 
		/// The task is done.
		/// </summary>
		completed,
		/// <summary> 
		/// The task has been created, but not yet assigned to an asset.
		/// </summary>
		created,
		/// <summary> 
		/// The asset is on the way to the task's location next.
		/// </summary>
		onroute,
		/// <summary> 
		/// The task has been given to an asset (and delivered to the asset's primary device).
		/// </summary>
		queued
	}

	/// <summary> 
	/// A task assigned to an asset which represents a coordinate on the map which must be visited.
	/// </summary>
	public partial class DispatchTask {
		/// <summary>
		/// The street address of where the task must be completed.
		/// </summary>
		/// <remarks>max-length: 500</remarks>
		public string address;
		/// <summary>
		/// The date/time stamp of when the asset arrived at this task.
		/// </summary>
		public DateTime? arrived;
		/// <summary>
		/// The asset to which this task belongs.
		/// </summary>
		public ulong asset;
		/// <summary>
		/// The company to which this task belongs.
		/// </summary>
		public ulong company;
		/// <summary>
		/// The date/time stamp of when this task was completed.
		/// </summary>
		public DateTime? completed;
		/// <summary>
		/// When this task was created.
		/// </summary>
		public DateTime created;
		/// <summary>
		/// The optional expected duration of the work for this task.
		/// </summary>
		public TimeSpan? duration;
		/// <summary>
		/// The optional estimated time of arrival for the asset.
		/// </summary>
		public DateTime? eta;
		/// <summary>
		/// Unique identifier of this task.
		/// </summary>
		public ulong id;
		/// <summary>
		/// Instructions (filled out by dispatcher) for the field-employee to help them completed the task.
		/// </summary>
		public string instructions;
		/// <summary>
		/// The lat/long coordinates of where the task must be completed.
		/// </summary>
		public LatLng latlng;
		/// <summary>
		/// The name of this task or the work needed to be performed.
		/// </summary>
		/// <remarks>max-length: 100</remarks>
		public string name;
		/// <summary>
		/// Notes about the status of the work filled in by field-employee.
		/// </summary>
		public string notes;
		/// <summary>
		/// An optional place which can be used as a template instead of providing lat/long coordinates and a street address.
		/// </summary>
		public ulong? place;
		/// <summary>
		/// A custom field used to refer to an external system. Examples are a work order, pick-up, waybill, etc...
		/// </summary>
		/// <remarks>max-length: 100</remarks>
		public string reference;
		/// <summary>
		/// The name of the person who signed the task's completion.
		/// </summary>
		/// <remarks>max-length: 100</remarks>
		public string signatory;
		/// <summary>
		/// Indicates whether the task has a signature.
		/// </summary>
		public bool signature;
		/// <summary>
		/// The progress of this task.
		/// </summary>
		public TaskStatus status;
		/// <summary>
		/// Either the user's login, or provider's identifier that changed this task
		/// </summary>
		public string updatedBy;
		/// <summary>
		/// Timestamp from the last change made to this task
		/// </summary>
		public DateTime updatedUtc;
		/// <summary>
		/// Object version keys used to validate synchronization for all object properties.
		/// </summary>
		public uint[] v;
	}

	/// <summary> 
	/// Relevant details of a task which was deleted.
	/// </summary>
	public partial class DispatchTaskDeleted {
		/// <summary>
		/// The asset to which this task belonged.
		/// </summary>
		public ulong asset;
		/// <summary>
		/// The company to which the asset belonged.
		/// </summary>
		public ulong company;
		/// <summary>
		/// Unique identifier of this task.
		/// </summary>
		public ulong id;
	}
	#endregion Dispatch Tasks
	#region Hours of Service
	/// <summary>
	/// Driving cycle behaviour
	/// </summary>
	public enum HosCycle {
		/// <summary> 
		/// Canada 120 hour / 14 days (Cycle 2)
		/// Canada North/South 120 hour / 14 days (Cycle 2)
		/// </summary>
		canada120h14d,
		/// <summary> 
		/// Canada South 70 hour / 7 days (Cycle 1)
		/// Canada South 70 hour / 7 days (Cycle 1)
		/// </summary>
		canada70h7d,
		/// <summary> 
		/// Canada North 80 hour / 7 days (Cycle 1)
		/// Canada North 80 hour / 7 days (Cycle 1)
		/// </summary>
		canada80h7d,
		/// <summary> 
		/// USA Federal 60 hour / 7 days
		/// USA Federal 60 hour / 7 days
		/// </summary>
		usa60h7d,
		/// <summary> 
		/// USA Federal 70 hour / 8 days
		/// USA Federal 70 hour / 8 days
		/// </summary>
		usa70h8d,
		/// <summary> 
		/// USA Alaska 70 hour / 7 days
		/// USA Alaska 70 hour / 7 days
		/// </summary>
		usaAlaska70h7d,
		/// <summary> 
		/// USA Alaska 80 hour / 8 days
		/// USA Alaska 80 hour / 8 days
		/// </summary>
		usaAlaska80h8d,
		/// <summary> 
		/// USA California 112 hours / 8 days (farm products)
		/// USA California 112 hours / 8 days (farm products)
		/// </summary>
		usaCalifornia112h8d,
		/// <summary> 
		/// USA California 80 hour / 8 days
		/// USA California 80 hour / 8 days
		/// </summary>
		usaCalifornia80h8d,
		/// <summary> 
		/// USA Texas 70 hour / 7 days
		/// USA Texas 70 hour / 7 days
		/// </summary>
		usaTexas70h7d
	}

	/// <summary>
	/// Method of transferring HoS E-logs to the authorities
	/// </summary>
	public enum HosDataTransferType {
		/// <summary> 
		/// MTO via Email
		/// </summary>
		canadaEmail,
		/// <summary> 
		/// FMCSA via Email
		/// </summary>
		usaEmail,
		/// <summary> 
		/// FMCSA via Webservice portal
		/// </summary>
		usaWebservice
	}

	/// <summary>
	/// Shows where a defect was located on the rig.
	/// </summary>
	public enum HosDefectArea {
		/// <summary> 
		/// Defect on the vehicle.
		/// </summary>
		tractor,
		/// <summary> 
		/// Defect on one of the trailers.
		/// </summary>
		trailer
	}

	/// <summary>
	/// Resolution status for trip inspection defects
	/// </summary>
	public enum HosDefectStatus {
		/// <summary> 
		/// Issue fixed or resolved
		/// </summary>
		corrected,
		/// <summary> 
		/// Issue does not affect operation
		/// </summary>
		harmless,
		/// <summary> 
		/// Does not need to be corrected
		/// </summary>
		satisfactory
	}

	/// <summary>
	/// Data diagnostic events
	/// </summary>
	public enum HosDiagnosticCode {
		/// <summary> 
		/// Data transfer data diagnostic event
		/// </summary>
		dataTransfer,
		/// <summary> 
		/// Engine synchronization data diagnostic event
		/// </summary>
		engine,
		/// <summary> 
		/// Missing required data elements data diagnostic event
		/// </summary>
		missingData,
		/// <summary> 
		/// Other ELD identified diagnostic event
		/// </summary>
		other,
		/// <summary> 
		/// Power data diagnostic event
		/// </summary>
		power,
		/// <summary> 
		/// Unidentified driving records data diagnostic event
		/// </summary>
		unidentified
	}

	/// <summary>
	/// Specifies the nature of the change indicated in "Diagnostic" event type
	/// </summary>
	public enum HosEventCodeDiagnostic {
		/// <summary> 
		/// 
		/// Unknown duty status
		/// </summary>
		unknown = -1,
		/// <summary> 
		/// 
		/// An ELD malfunction logged
		/// </summary>
		malfunctionLogged = 1,
		/// <summary> 
		/// 
		/// An ELD malfunction cleared
		/// </summary>
		malfunctionCleared = 2,
		/// <summary> 
		/// 
		/// A data diagnostic event logged
		/// </summary>
		diagnosticLogged = 3,
		/// <summary> 
		/// 
		/// An data diagnostic event cleared
		/// </summary>
		diagnosticCleared = 4
	}

	/// <summary>
	/// Specifies the nature of the change indicated in "Duty Status" event type
	/// </summary>
	public enum HosEventCodeDutyStatus {
		/// <summary> 
		/// 
		/// Unknown duty status
		/// </summary>
		unknown = -1,
		/// <summary> 
		/// 
		/// Driver's duty status changed to "Off-duty"
		/// </summary>
		offDuty = 1,
		/// <summary> 
		/// 
		/// Driver's duty status changed to "Sleeper Berth"
		/// </summary>
		sleeperBerth = 2,
		/// <summary> 
		/// 
		/// Driver's duty status changed to "Driving"
		/// </summary>
		driving = 3,
		/// <summary> 
		/// 
		/// Driver's duty status changed to "On-duty not driving"
		/// </summary>
		onDuty = 4
	}

	/// <summary>
	/// Specifies the nature of the change indicated in "Engine Power" event type
	/// </summary>
	public enum HosEventCodeEnginePower {
		/// <summary> 
		/// 
		/// Unknown duty status
		/// </summary>
		unknown = -1,
		/// <summary> 
		/// 
		/// Engine power-up with conventional location precision
		/// </summary>
		powerupConventional = 1,
		/// <summary> 
		/// 
		/// Engine power-up with reduced location precision
		/// </summary>
		powerupReduced = 2,
		/// <summary> 
		/// 
		/// Engine shut-down with conventional location precision
		/// </summary>
		shutdownConventional = 3,
		/// <summary> 
		/// 
		/// Engine shut-down with reduced location precision
		/// </summary>
		shutdownReduced = 4
	}

	/// <summary>
	/// Specifies the nature of the change indicated in "Intermediate" event type
	/// </summary>
	public enum HosEventCodeIntermediate {
		/// <summary> 
		/// 
		/// Unknown
		/// </summary>
		unknown = -1,
		/// <summary> 
		/// 
		/// Intermediate log with conventional location precision
		/// </summary>
		intermediateConventional = 1,
		/// <summary> 
		/// 
		/// Intermediate log with reduced location precision
		/// </summary>
		intermediateReduced = 2
	}

	/// <summary>
	/// Specifies the nature of the change indicated in "Login" event type
	/// </summary>
	public enum HosEventCodeLogin {
		/// <summary> 
		/// 
		/// Unknown
		/// </summary>
		unknown = -1,
		/// <summary> 
		/// 
		/// Authenticated driver's ELD login activity
		/// </summary>
		login = 1,
		/// <summary> 
		/// 
		/// Authenticated driver's ELD logout activity
		/// </summary>
		logout = 2
	}

	/// <summary>
	/// Specifies the nature of the change indicated in "Other" event type
	/// </summary>
	public enum HosEventCodeOther {
		/// <summary> 
		/// 
		/// Unknown
		/// </summary>
		unknown = -1,
		/// <summary> 
		/// 
		/// Driver indication for PC, YM and WT cleared
		/// </summary>
		none = 0,
		/// <summary> 
		/// 
		/// Driver indicates "Authorized Personal use of CMV"
		/// </summary>
		personalConveyance = 1,
		/// <summary> 
		/// 
		/// Driver indicates Yard moves"
		/// </summary>
		yardMoves = 2
	}

	/// <summary>
	/// Indicates where the event was generated.
	/// </summary>
	public enum HosEventOrigin {
		/// <summary> 
		/// Automatically recorded by ELD
		/// </summary>
		automatic,
		/// <summary> 
		/// Entered or edited by driver
		/// </summary>
		driver,
		/// <summary> 
		/// Assumed from unidentified driver profile
		/// </summary>
		unidentified,
		/// <summary> 
		/// Edit requested by an authenticated user other than driver
		/// </summary>
		user
	}

	/// <summary>
	/// Event record status
	/// </summary>
	public enum HosEventStatus {
		/// <summary> 
		/// Active
		/// </summary>
		active,
		/// <summary> 
		/// Inactive - changed
		/// </summary>
		inactiveChanged,
		/// <summary> 
		/// Inactive - change rejected
		/// </summary>
		inactiveChangeRejected,
		/// <summary> 
		/// Inactive - change requested
		/// </summary>
		inactiveChangeRequested
	}

	/// <summary>
	/// Event record type
	/// </summary>
	public enum HosEventType {
		/// <summary> 
		/// 
		/// A change in driver's duty-status
		/// </summary>
		dutyStatus = 1,
		/// <summary> 
		/// 
		/// An intermediate log
		/// </summary>
		intermediate = 2,
		/// <summary> 
		/// 
		/// A change in driver's indication of authorized personal use of CMV or yard moves
		/// </summary>
		other = 3,
		/// <summary> 
		/// 
		/// A driver's certification/re-certification of records
		/// </summary>
		certification = 4,
		/// <summary> 
		/// 
		/// A driver's login/logout activity
		/// </summary>
		login = 5,
		/// <summary> 
		/// 
		/// CMV's engine power up/shutdown activity
		/// </summary>
		enginePower = 6,
		/// <summary> 
		/// 
		/// A malfunction or data diagnostic detection occurence
		/// </summary>
		diagnostic = 7
	}

	/// <summary>
	/// Compliance Malfunction events
	/// </summary>
	public enum HosMalfunctionCode {
		/// <summary> 
		/// Engine synchronization compliance malfunction
		/// </summary>
		E,
		/// <summary> 
		/// Positioning compliance malfunction
		/// </summary>
		L,
		/// <summary> 
		/// Other ELD detected malfunction
		/// </summary>
		O,
		/// <summary> 
		/// Power compliance malfunction
		/// </summary>
		P,
		/// <summary> 
		/// Data recording compliance malfunction
		/// </summary>
		R,
		/// <summary> 
		/// Data transfer compliance malfunction
		/// </summary>
		S,
		/// <summary> 
		/// Timing compliance malfunction
		/// </summary>
		T
	}

	/// <summary>
	/// Violations
	/// </summary>
	public enum HosViolationType {
		/// <summary> 
		/// 24 Hour Break Required
		/// </summary>
		break_24,
		/// <summary> 
		/// 30 Minute Break Required
		/// </summary>
		break_30,
		/// <summary> 
		/// 120 Hour Cycle Limit
		/// </summary>
		cycle_120,
		/// <summary> 
		/// 60 Hour Cycle Limit
		/// </summary>
		cycle_60,
		/// <summary> 
		/// 70 Hour Cycle Limit
		/// </summary>
		cycle_70,
		/// <summary> 
		/// Daily 10 Hour Off-Duty Required
		/// </summary>
		daily_break_10,
		/// <summary> 
		/// Daily 13 Hour Driving Limit
		/// </summary>
		daily_driving_13,
		/// <summary> 
		/// Daily 14 Hour On-Duty Limit
		/// </summary>
		daily_duty_14,
		/// <summary> 
		/// 10 Hour Driving Limit
		/// </summary>
		driving_10,
		/// <summary> 
		/// 11 Hour Driving Limit
		/// </summary>
		driving_11,
		/// <summary> 
		/// 13 Hour Driving Limit
		/// </summary>
		driving_13,
		/// <summary> 
		/// 14 Hour Shift Limit
		/// </summary>
		duty_14,
		/// <summary> 
		/// 15 Hour On-Duty Limit
		/// </summary>
		duty_15,
		/// <summary> 
		/// 16 Hour Shift Limit
		/// </summary>
		duty_16,
		/// <summary> 
		/// No violation
		/// </summary>
		none
	}

	/// <summary> 
	/// An operator registered to transport goods or people.
	/// </summary>
	public partial class HosCarrier {
		/// <summary>
		/// The carrier's physical or incorporated address.
		/// </summary>
		public StreetAddress address;
		/// <summary>
		/// Identity code supplied by a regulatory/government body (DOT ID).
		/// </summary>
		public Dictionary<string, string> codes;
		/// <summary>
		/// The company to which this carrier information belongs.
		/// </summary>
		public ulong company;
		/// <summary>
		/// Contact information for the carrier.
		/// </summary>
		public ulong? contact;
		/// <summary>
		/// Unique identifier of the Carrier.
		/// </summary>
		public ulong id;
		/// <summary>
		/// A Picture of the carrier's logo.
		/// </summary>
		public ulong? logo;
		/// <summary>
		/// The carrier's registered name.
		/// </summary>
		/// <remarks>max-length: 100</remarks>
		public string name;
		/// <summary>
		/// Notes.
		/// </summary>
		public string notes;
		/// <summary>
		/// Default shift cycle used by the drivers
		/// </summary>
		public HosCycle shiftCycle;
		/// <summary>
		/// The local timezone of the carrier's address.
		/// </summary>
		public string timezone;
		/// <summary>
		/// Object version keys used to validate synchronization for all object properties.
		/// </summary>
		public uint[] v;
	}

	/// <summary> 
	/// Relevant details of a carrier which was deleted.
	/// </summary>
	public partial class HosCarrierDeleted {
		/// <summary>
		/// The company to which this carrier belonged.
		/// </summary>
		public ulong company;
		/// <summary>
		/// Unique identifier of this carrier.
		/// </summary>
		public ulong id;
	}

	/// <summary> 
	/// An ELD event record
	/// </summary>
	public partial class HosEvent {
		/// <summary>
		/// Geolocation/Written address by driver of where the event took place
		/// </summary>
		public StreetAddress address;
		/// <summary>
		/// The carrier information to which this event belongs.
		/// </summary>
		public ulong carrier;
		/// <summary>
		/// Date of the records being certified or re-certified by the driver
		/// </summary>
		public DateTime? certificationDate;
		/// <summary>
		/// Event code
		/// </summary>
		public byte code;
		/// <summary>
		/// The company to which this event belongs.
		/// </summary>
		public ulong company;
		/// <summary>
		/// Data Diagnostic Event Code
		/// </summary>
		public HosDiagnosticCode? diagnosticCode;
		/// <summary>
		/// Data Diagnostic Event Indicator Status
		/// </summary>
		public bool? diagnosticStatus;
		/// <summary>
		/// Distance since last valid coordinates. An integer value between 0 and 6
		/// </summary>
		public float? distanceSinceGPSFix;
		/// <summary>
		/// When the event took place
		/// </summary>
		public DateTime dts;
		/// <summary>
		/// Odometer reading of the tractor at the time of the event
		/// </summary>
		public double? engineHours;
		/// <summary>
		/// Unique identifier of the event.
		/// </summary>
		public ulong id;
		/// <summary>
		/// Driver status - Primary/Co-driver
		/// </summary>
		public bool? isCoDriver;
		/// <summary>
		/// Single character 'X', single character 'M', single character 'E', or 2-decimal point precision value between -90.00 and 90.00
		/// </summary>
		public string latitude;
		/// <summary>
		/// Single character 'X', single character 'M', single character 'E', or 2-decimal point precision value between -179.99 and 180.00
		/// </summary>
		public string longitude;
		/// <summary>
		/// Malfunction Indicator Code
		/// </summary>
		public string malfunctionCode;
		/// <summary>
		/// Malfunction Indicator Status
		/// </summary>
		public bool? malfunctionStatus;
		/// <summary>
		/// Comments/Annotations entered by the driver/user
		/// </summary>
		public string notes;
		/// <summary>
		/// Odometer reading of the tractor at the time of the event
		/// </summary>
		public double? odometer;
		/// <summary>
		/// Event Record origin
		/// </summary>
		public HosEventOrigin? origin;
		/// <summary>
		/// Driver associated with this event
		/// </summary>
		public ulong? person;
		/// <summary>
		/// Event Sequence Id Number
		/// </summary>
		public ushort seqNo;
		/// <summary>
		/// Driver's shift cycle
		/// </summary>
		public HosCycle? shiftCycle;
		/// <summary>
		/// Event Record status
		/// </summary>
		public HosEventStatus status;
		/// <summary>
		/// Event type
		/// </summary>
		public HosEventType type;
		/// <summary>
		/// Object version keys used to validate synchronization for all object properties.
		/// </summary>
		public uint[] v;
		/// <summary>
		/// Vehicle associated with this event
		/// </summary>
		public ulong? vehicle;
		/// <summary>
		/// Violation Status
		/// </summary>
		public bool? violationStatus;
		/// <summary>
		/// Violation Type
		/// </summary>
		public HosViolationType? violationType;
	}

	/// <summary> 
	/// A pre- or post-Trip inspection report.
	/// </summary>
	public partial class HosInspection {
		/// <summary>
		/// Written address by driver of where the inspection took place
		/// </summary>
		public StreetAddress address;
		/// <summary>
		/// The carrier information to which this inspection report belongs.
		/// </summary>
		public ulong carrier;
		/// <summary>
		/// The company to which this inspection report belongs.
		/// </summary>
		public ulong company;
		/// <summary>
		/// List of all defects
		/// </summary>
		public HosInspectionDefect[] defects;
		/// <summary>
		/// Unique identifier of the inspection reports.
		/// </summary>
		public ulong id;
		/// <summary>
		/// When the inspection took place
		/// </summary>
		public DateTime inspectedOn;
		/// <summary>
		/// Coordinates where the inspection took place
		/// </summary>
		public LatLng latlng;
		/// <summary>
		/// Odometer reading of the tractor at the time of the inspection
		/// </summary>
		public double odometer;
		/// <summary>
		/// Driver who conducted the inspection
		/// </summary>
		public ulong person;
		/// <summary>
		/// Status of all defects
		/// </summary>
		public HosDefectStatus status;
		/// <summary>
		/// Name of the (optional) technician/mechanic doing work or inspection.
		/// </summary>
		/// <remarks>max-length: 100</remarks>
		public string technician;
		/// <summary>
		/// If the defects needed to be addressed by a technician/mechanic, his signature will happen at a different time.
		/// </summary>
		public DateTime? techSigned;
		/// <summary>
		/// Between 0 and 2 trailers being inspected
		/// </summary>
		public ulong[] trailers;
		/// <summary>
		/// Object version keys used to validate synchronization for all object properties.
		/// </summary>
		public uint[] v;
		/// <summary>
		/// Tractor being inspected
		/// </summary>
		public ulong vehicle;
	}

	/// <summary> 
	/// Details of a defect found during a trip inspection
	/// </summary>
	public partial class HosInspectionDefect {
		/// <summary>
		/// Shows where a defect was located on the rig.
		/// </summary>
		public HosDefectArea area;
		/// <summary>
		/// The kind of defect
		/// </summary>
		public string category;
		/// <summary>
		/// A text description of the defect.
		/// </summary>
		public string description;
		/// <summary>
		/// A list of major flaws as noted for Canadian standards.
		/// </summary>
		public string[] major;
		/// <summary>
		/// A list of minor flaws as noted for Canadian standards.
		/// </summary>
		public string[] minor;
	}

	/// <summary> 
	/// Relevant details of a trip inspection which was deleted.
	/// </summary>
	public partial class HosInspectionDeleted {
		/// <summary>
		/// The company to which this trip inspection belonged.
		/// </summary>
		public ulong company;
		/// <summary>
		/// Unique identifier of this trip inspection.
		/// </summary>
		public ulong id;
	}
	#endregion Hours of Service
	#region Icons
	/// <summary>
	/// The layers of the map used to visualize the icon.
	/// </summary>
	public enum IconLayer {
		/// <summary> 
		/// An SVG only layer for special drawing controls. Icons should not use this layer.
		/// </summary>
		drawings,
		/// <summary> 
		/// An HTML only layer for special drawing controls. Icons should not use this layer.
		/// </summary>
		edits,
		/// <summary> 
		/// An SVG only layer just above the roads used for solid Places and accuracy radius fill.
		/// </summary>
		fills,
		/// <summary> 
		/// An HTML only layer intended for use by an icon's label.
		/// </summary>
		labels,
		/// <summary> 
		/// An HTML only layer intended for use by an icon's main images.
		/// </summary>
		markers,
		/// <summary> 
		/// An SVG only layer intended for use by shape and accuracy radius outlines.
		/// </summary>
		outlines,
		/// <summary> 
		/// An HTML only layer intended for use by an icon's drop-shadow.
		/// </summary>
		shadows
	}

	/// <summary> 
	/// A visual representation of a thing on a map or in a list.
	/// </summary>
	public partial class Icon {
		/// <summary>
		/// A noun to describe the type of thing represented. Like Truck, Car, Trailer, Hot-Air Balloon, etc...
		/// </summary>
		/// <remarks>max-length: 100</remarks>
		public string category;
		/// <summary>
		/// The company to which this icon belongs.
		/// </summary>
		public ulong company;
		/// <summary>
		/// Indicates whether this icon is available to child companies.
		/// </summary>
		public bool global;
		/// <summary>
		/// The images used to show the detail of this icon.
		/// </summary>
		public IconGlyph[] glyphs;
		/// <summary>
		/// Unique identifier of this icon.
		/// </summary>
		public ulong id;
		/// <summary>
		/// Definition for the name bubble above the icon on a map.
		/// </summary>
		public IconLabel label;
		/// <summary>
		/// A specific adjective to describe the thing. Like Blue, Red, Empty, Full, etc...
		/// </summary>
		/// <remarks>max-length: 100</remarks>
		public string name;
		/// <summary>
		/// Notes.
		/// </summary>
		public string notes;
		/// <summary>
		/// A list of things that this icon can be used to represent. Like asset, place, user, etc...
		/// </summary>
		public string[] usage;
		/// <summary>
		/// Object version keys used to validate synchronization for all object properties.
		/// </summary>
		public uint[] v;
	}

	/// <summary> 
	/// Relevant details of an icon which was deleted.
	/// </summary>
	public partial class IconDeleted {
		/// <summary>
		/// The company to which this icon belonged.
		/// </summary>
		public ulong company;
		/// <summary>
		/// Unique identifier of this icon.
		/// </summary>
		public ulong id;
	}
	/// <summary> 
	/// The image source and defined status tags which need to be applied to an asset in order to show the image.
	/// </summary>
	public partial class IconGlyph {
		/// <summary>
		/// The offset from the lat/long in pixels.
		/// </summary>
		public Point anchor;
		/// <summary>
		/// The layer on which this glyph is displayed.
		/// </summary>
		public IconLayer layer;
		/// <summary>
		/// Indicates that this glyph rotate based on GPS bearing.
		/// </summary>
		public bool rotates;
		/// <summary>
		/// Size of the glyph in pixels.
		/// </summary>
		public Size size;
		/// <summary>
		/// Path to the image.
		/// </summary>
		public string src;
		/// <summary>
		/// A list of codified status tag names. Any of the tags must be applied to the asset for the image to appear.
		/// </summary>
		public string[] tags;
		/// <summary>
		/// The z-order of this glyph compared to other glyphs on the same layer.
		/// </summary>
		public ushort zIndex;
	}

	/// <summary> 
	/// Definition for the name bubble above the icon on a map.
	/// </summary>
	public partial class IconLabel {
		/// <summary>
		/// Determines which corner of the label is attached to the anchor.
		/// </summary>
		public string align;
		/// <summary>
		/// The offset from the lat/long in pixels.
		/// </summary>
		public Point anchor;
		/// <summary>
		/// Background colour of the label.
		/// </summary>
		public string colour;
	}

	#endregion Icons
	#region Maintenance
	/// <summary>
	/// The lifetime of a Maintenance Job
	/// </summary>
	public enum MaintenanceJobStatus {
		/// <summary> 
		/// The job was cancelled or was not necessary.
		/// </summary>
		cancelled,
		/// <summary> 
		/// Work is completed.
		/// </summary>
		completed,
		/// <summary> 
		/// The work was scheduled, but not yet done.
		/// </summary>
		pastdue,
		/// <summary> 
		/// The work will need to be performed soon.
		/// </summary>
		pending
	}

	/// <summary> 
	/// The detail for calculating Maintenance Schedule recurrence.
	/// </summary>
	public partial class MaintenanceInterval {
		/// <summary>
		/// The Vehicle or Trailer to which this recurrence detail belongs.
		/// </summary>
		public ulong asset;
		/// <summary>
		/// The date of the last calculation.
		/// </summary>
		public DateTime date;
		/// <summary>
		/// The operating time at the time of the last calculation.
		/// </summary>
		public double engineHours;
		/// <summary>
		/// The last "completed" job related to this schedule interval.
		/// </summary>
		public ulong lastJob;
		/// <summary>
		/// The odometer at the time of the last calculation.
		/// </summary>
		public double odometer;
	}

	/// <summary> 
	/// Historical service work performed on a Vehicle or Trailer
	/// </summary>
	public partial class MaintenanceJob {
		/// <summary>
		/// The Vehicle or Trailer to which this job belongs
		/// </summary>
		public ulong asset;
		/// <summary>
		/// The company to which this Vehicle or Trailer belongs
		/// </summary>
		public ulong company;
		/// <summary>
		/// When was this job created.
		/// </summary>
		public DateTime? completed;
		/// <summary>
		/// How much the job cost in dollars.
		/// </summary>
		public double cost;
		/// <summary>
		/// When was this job created.
		/// </summary>
		public DateTime created;
		/// <summary>
		/// Time it took to complete the job.
		/// </summary>
		public TimeSpan duration;
		/// <summary>
		/// The operating time at the time of the service.
		/// </summary>
		public double? engineHours;
		/// <summary>
		/// The name of the garage or service facility where the work is done.
		/// </summary>
		/// <remarks>max-length: 100</remarks>
		public string garage;
		/// <summary>
		/// Unique identifier
		/// </summary>
		public ulong id;
		/// <summary>
		/// The work being done. Like "oil change".
		/// </summary>
		/// <remarks>max-length: 100</remarks>
		public string name;
		/// <summary>
		/// Notes about the job. Like "changed the oil and filter".
		/// </summary>
		public string notes;
		/// <summary>
		/// The odometer at the time of the service.
		/// </summary>
		public double? odometer;
		/// <summary>
		/// Images taken while performing the work for reference.
		/// </summary>
		public ulong[] pictures;
		/// <summary>
		/// A reference code used to track this job
		/// </summary>
		/// <remarks>max-length: 100</remarks>
		public string reference;
		/// <summary>
		/// The Maintenance Schedule from which this job was created
		/// </summary>
		public ulong? schedule;
		/// <summary>
		/// The status of this job.
		/// </summary>
		public MaintenanceJobStatus status;
		/// <summary>
		/// The mechanic who performed the work.
		/// </summary>
		/// <remarks>max-length: 100</remarks>
		public string technician;
		/// <summary>
		/// Object version keys used to validate synchronization for all object properties.
		/// </summary>
		public uint[] v;
	}

	/// <summary> 
	/// Relevant details of a maintenance job which was deleted.
	/// </summary>
	public partial class MaintenanceJobDeleted {
		/// <summary>
		/// The asset to which this maintenance job belonged.
		/// </summary>
		public ulong asset;
		/// <summary>
		/// The company to which this maintenance job belonged.
		/// </summary>
		public ulong company;
		/// <summary>
		/// Unique identifier of this maintenance job.
		/// </summary>
		public ulong id;
		/// <summary>
		/// The maintenance schedule of this maintenance job.
		/// </summary>
		public ulong schedule;
	}

	/// <summary> 
	/// Recurring service work
	/// </summary>
	public partial class MaintenanceSchedule {
		/// <summary>
		/// The company to which this schedule belongs
		/// </summary>
		public ulong company;
		/// <summary>
		/// The estimated cost for the job cost in dollars.
		/// </summary>
		public double cost;
		/// <summary>
		/// The estimated time for the job.
		/// </summary>
		public TimeSpan duration;
		/// <summary>
		/// The fill/background colour of the icon. Should be a hex colour in the format #RRGGBB.
		/// </summary>
		/// <remarks>max-length: 22</remarks>
		public string fill;
		/// <summary>
		/// The name of the garage or service facility where the work is done.
		/// </summary>
		/// <remarks>max-length: 100</remarks>
		public string garage;
		/// <summary>
		/// The name of the symbol for this report.
		/// </summary>
		/// <remarks>max-length: 22</remarks>
		public codified graphic;
		/// <summary>
		/// Unique identifier
		/// </summary>
		public ulong id;
		/// <summary>
		/// The per-asset details calculated by the system to help predict the creation of Maintenance Jobs.
		/// The object's keys follow the uint64 format.
		/// </summary>
		/// <seealso cref="uint64" />
		public Dictionary<ulong, MaintenanceInterval> intervals;
		/// <summary>
		/// The name of the work to be done. Like "oil change".
		/// </summary>
		/// <remarks>max-length: 100</remarks>
		public string name;
		/// <summary>
		/// Notes about the work to be done. Like "change the oil and oil filter".
		/// </summary>
		public string notes;
		/// <summary>
		/// List of Users to send notifications.
		/// </summary>
		public string[] notify;
		/// <summary>
		/// The number of days in advance to predict a job will become pending.
		/// </summary>
		/// <remarks>min-value: 5</remarks>
		/// <remarks>max-value: 180</remarks>
		public uint predictionDays;
		/// <summary>
		/// The number of days between service visits.
		/// </summary>
		public uint? recurDays;
		/// <summary>
		/// The amount of mileage between service visits.
		/// </summary>
		public double? recurDistance;
		/// <summary>
		/// The number of operating hours between service visits.
		/// </summary>
		public double? recurEngineHours;
		/// <summary>
		/// A reference code used to track this job
		/// </summary>
		/// <remarks>max-length: 100</remarks>
		public string reference;
		/// <summary>
		/// Outline and graphic colour. Should be a hex colour in the format #RRGGBB.
		/// </summary>
		/// <remarks>max-length: 22</remarks>
		public string stroke;
		/// <summary>
		/// The targeting expression to select which Vehicles and Trailers require this maintenance work.
		/// </summary>
		public expression targets;
		/// <summary>
		/// Object version keys used to validate synchronization for all object properties.
		/// </summary>
		public uint[] v;
	}

	/// <summary> 
	/// Relevant details of a maintenance schedule which was deleted.
	/// </summary>
	public partial class MaintenanceScheduleDeleted {
		/// <summary>
		/// The company to which this maintenance schedule belonged.
		/// </summary>
		public ulong company;
		/// <summary>
		/// Unique identifier of this maintenance schedule.
		/// </summary>
		public ulong id;
	}
	#endregion Maintenance
	#region Messaging
	/// <summary>
	/// The priority of the alert.
	/// </summary>
	public enum AlertPriority {
		/// <summary> 
		/// Sends before low and normal priority alerts.
		/// </summary>
		high,
		/// <summary> 
		/// Sends when no other alerts are pending in the queue.
		/// </summary>
		low,
		/// <summary> 
		/// Sends in when there are no high priority alerts in the queue.
		/// </summary>
		normal
	}

	/// <summary>
	/// Memos have a lifetime and each status represents a memos's progress through it's life.
	/// </summary>
	public enum MemoStatus {
		/// <summary> 
		/// Memo has been responded to or acknowledged by the recipient.
		/// </summary>
		acknowledged,
		/// <summary> 
		/// Memo sent, but returned with error from receiving server.
		/// </summary>
		bounceback,
		/// <summary> 
		/// Waiting to be sent.
		/// </summary>
		created,
		/// <summary> 
		/// Failed to send.
		/// </summary>
		failed,
		/// <summary> 
		/// Sent or received.
		/// </summary>
		processed,
		/// <summary> 
		/// Failed to send because too many memos were sent.
		/// </summary>
		throttled
	}

	/// <summary>
	/// The kind of protocol used for this memo.
	/// </summary>
	public enum MemoType {
		/// <summary> 
		/// Apple Push Notification Service
		/// </summary>
		apn,
		/// <summary> 
		/// Email
		/// </summary>
		email,
		/// <summary> 
		/// Google Cloud Message
		/// </summary>
		gcm,
		/// <summary> 
		/// Garmin/Magellan/etc (Personal Navigation Device)
		/// </summary>
		pnd,
		/// <summary> 
		/// Short Message Service (text message)
		/// </summary>
		sms,
		/// <summary> 
		/// WebSocket alert message
		/// </summary>
		socket,
		/// <summary> 
		/// If the type of memo has not yet been determined, or there was an error determining its type.
		/// </summary>
		unknown
	}

	/// <summary>
	/// The name of folder where the message is stored.
	/// </summary>
	public enum MessageFolder {
		/// <summary> 
		/// The archive contains all previous messages, but must be queried from disk for retrieval.
		/// </summary>
		archive,
		/// <summary> 
		/// The inbox is loaded quickly from memory, but messages regularly move to the archive.
		/// </summary>
		inbox
	}

	/// <summary> 
	/// An automatically generated notification sent to a user by the system.
	/// </summary>
	public partial class AssetAlert {
		/// <summary>
		/// The asset to which this message relates.
		/// </summary>
		public ulong asset;
		/// <summary>
		/// The main contents of the memo.
		/// </summary>
		public string body;
		/// <summary>
		/// The company to which this memo belongs.
		/// </summary>
		public ulong company;
		/// <summary>
		/// Date/time stamp of when the memo was delivered (or sent if delivery information unavailable).
		/// </summary>
		public DateTime? delivered;
		/// <summary>
		/// Sender address
		/// </summary>
		/// <remarks>min-length: 6</remarks>
		/// <remarks>max-length: 254</remarks>
		public string from;
		/// <summary>
		/// Unique identifier of this memo.
		/// </summary>
		public ulong id;
		/// <summary>
		/// Protocol type
		/// </summary>
		public MemoType kind;
		/// <summary>
		/// The priority for which this message must send.
		/// </summary>
		public AlertPriority priority;
		/// <summary>
		/// Date/time stamp of when the memo was processed.
		/// </summary>
		public DateTime? processed;
		/// <summary>
		/// Lifetime status
		/// </summary>
		public MemoStatus status;
		/// <summary>
		/// The subject of this message.
		/// </summary>
		/// <remarks>max-length: 100</remarks>
		public string subject;
		/// <summary>
		/// Recipient address
		/// </summary>
		/// <remarks>min-length: 6</remarks>
		/// <remarks>max-length: 254</remarks>
		public string to;
		/// <summary>
		/// The user who sent/received this message.
		/// </summary>
		/// <remarks>max-length: 254</remarks>
		public email user;
		/// <summary>
		/// Object version keys used to validate synchronization for all object properties.
		/// </summary>
		public uint[] v;
	}

	/// <summary> 
	/// A conversational message between users and assets.
	/// </summary>
	public partial class Message {
		/// <summary>
		/// The asset to which this message relates.
		/// </summary>
		public ulong asset;
		/// <summary>
		/// The main contents of the memo.
		/// </summary>
		public string body;
		/// <summary>
		/// The company to which this memo belongs.
		/// </summary>
		public ulong company;
		/// <summary>
		/// Date/time stamp of when the memo was delivered (or sent if delivery information unavailable).
		/// </summary>
		public DateTime? delivered;
		/// <summary>
		/// The folder under which this message is stored.
		/// </summary>
		public MessageFolder folder;
		/// <summary>
		/// Sender address
		/// </summary>
		/// <remarks>min-length: 6</remarks>
		/// <remarks>max-length: 254</remarks>
		public string from;
		/// <summary>
		/// Unique identifier of this memo.
		/// </summary>
		public ulong id;
		/// <summary>
		/// Indicates that this is a received message instead of a sent message.
		/// </summary>
		public bool incoming;
		/// <summary>
		/// Protocol type
		/// </summary>
		public MemoType kind;
		/// <summary>
		/// Date/time stamp of when the memo was processed.
		/// </summary>
		public DateTime? processed;
		/// <summary>
		/// The user that read this message. This field is blank/null when unread.
		/// </summary>
		/// <remarks>max-length: 254</remarks>
		public email readBy;
		/// <summary>
		/// Lifetime status
		/// </summary>
		public MemoStatus status;
		/// <summary>
		/// The subject of this message.
		/// </summary>
		/// <remarks>max-length: 100</remarks>
		public string subject;
		/// <summary>
		/// Recipient address
		/// </summary>
		/// <remarks>min-length: 6</remarks>
		/// <remarks>max-length: 254</remarks>
		public string to;
		/// <summary>
		/// The user who sent/received this message.
		/// </summary>
		/// <remarks>max-length: 254</remarks>
		public email user;
		/// <summary>
		/// Object version keys used to validate synchronization for all object properties.
		/// </summary>
		public uint[] v;
	}

	/// <summary> 
	/// Relevant details of a message which was deleted.
	/// </summary>
	public partial class MessageDeleted {
		/// <summary>
		/// The asset to which this message belonged.
		/// </summary>
		public ulong asset;
		/// <summary>
		/// The company to which the asset belonged.
		/// </summary>
		public ulong company;
		/// <summary>
		/// Unique identifier of this message.
		/// </summary>
		public ulong id;
	}
	/// <summary> 
	/// A conversational message between a user and an asset's PND.
	/// </summary>
	public partial class PndMessage {
		/// <summary>
		/// The asset to which this message relates.
		/// </summary>
		public ulong asset;
		/// <summary>
		/// The main contents of the memo.
		/// </summary>
		public string body;
		/// <summary>
		/// The company to which this memo belongs.
		/// </summary>
		public ulong company;
		/// <summary>
		/// Date/time stamp of when the memo was delivered (or sent if delivery information unavailable).
		/// </summary>
		public DateTime? delivered;
		/// <summary>
		/// The folder under which this message is stored.
		/// </summary>
		public MessageFolder folder;
		/// <summary>
		/// Sender address
		/// </summary>
		/// <remarks>min-length: 6</remarks>
		/// <remarks>max-length: 254</remarks>
		public string from;
		/// <summary>
		/// Unique identifier of this memo.
		/// </summary>
		public ulong id;
		/// <summary>
		/// Indicates that this is a received message instead of a sent message.
		/// </summary>
		public bool incoming;
		/// <summary>
		/// Protocol type
		/// </summary>
		public MemoType kind;
		/// <summary>
		/// Date/time stamp of when the memo was processed.
		/// </summary>
		public DateTime? processed;
		/// <summary>
		/// The user that read this message. This field is blank/null when unread.
		/// </summary>
		/// <remarks>max-length: 254</remarks>
		public email readBy;
		/// <summary>
		/// A list of predefined responses a driver can easily read and click without distraction from the road.
		/// </summary>
		public string[] responses;
		/// <summary>
		/// Lifetime status
		/// </summary>
		public MemoStatus status;
		/// <summary>
		/// The subject of this message.
		/// </summary>
		/// <remarks>max-length: 100</remarks>
		public string subject;
		/// <summary>
		/// Recipient address
		/// </summary>
		/// <remarks>min-length: 6</remarks>
		/// <remarks>max-length: 254</remarks>
		public string to;
		/// <summary>
		/// The user who sent/received this message.
		/// </summary>
		/// <remarks>max-length: 254</remarks>
		public email user;
		/// <summary>
		/// Object version keys used to validate synchronization for all object properties.
		/// </summary>
		public uint[] v;
	}

	/// <summary> 
	/// A command sent to a device or GPS modem.
	/// </summary>
	public partial class ProviderCommand {
		/// <summary>
		/// The main contents of the memo.
		/// </summary>
		public string body;
		/// <summary>
		/// The company to which this memo belongs.
		/// </summary>
		public ulong company;
		/// <summary>
		/// Date/time stamp of when the memo was delivered (or sent if delivery information unavailable).
		/// </summary>
		public DateTime? delivered;
		/// <summary>
		/// Sender address
		/// </summary>
		/// <remarks>min-length: 6</remarks>
		/// <remarks>max-length: 254</remarks>
		public string from;
		/// <summary>
		/// Unique identifier of this memo.
		/// </summary>
		public ulong id;
		/// <summary>
		/// Protocol type
		/// </summary>
		public MemoType kind;
		/// <summary>
		/// Date/time stamp of when the memo was processed.
		/// </summary>
		public DateTime? processed;
		/// <summary>
		/// The device to which this command was sent.
		/// </summary>
		/// <remarks>max-length: 50</remarks>
		public string provider;
		/// <summary>
		/// Lifetime status
		/// </summary>
		public MemoStatus status;
		/// <summary>
		/// Recipient address
		/// </summary>
		/// <remarks>min-length: 6</remarks>
		/// <remarks>max-length: 254</remarks>
		public string to;
		/// <summary>
		/// Object version keys used to validate synchronization for all object properties.
		/// </summary>
		public uint[] v;
	}

	#endregion Messaging
	#region Pictures
	/// <summary> 
	/// An image stored by the system.
	/// </summary>
	public partial class Picture {
		/// <summary>
		/// The file-size on the disk.
		/// </summary>
		public ulong bytes;
		/// <summary>
		/// The company to which this image belongs.
		/// </summary>
		public ulong company;
		/// <summary>
		/// A list of focal points in the images like faces.
		/// </summary>
		public Square[] focals;
		/// <summary>
		/// Unique identifier of this image.
		/// </summary>
		public ulong id;
		/// <summary>
		/// The file name of this image.
		/// </summary>
		/// <remarks>max-length: 100</remarks>
		public string name;
		/// <summary>
		/// Notes about this image.
		/// </summary>
		public string notes;
		/// <summary>
		/// Size defined in pixels.
		/// </summary>
		public Size size;
		/// <summary>
		/// The URL/path to find this image.
		/// </summary>
		/// <remarks>max-length: 200</remarks>
		public string src;
		/// <summary>
		/// Object version keys used to validate synchronization for all object properties.
		/// </summary>
		public uint[] v;
	}

	/// <summary> 
	/// Relevant details of a picture which was deleted.
	/// </summary>
	public partial class PictureDeleted {
		/// <summary>
		/// The company to which this picture belonged.
		/// </summary>
		public ulong company;
		/// <summary>
		/// Unique identifier of this picture.
		/// </summary>
		public ulong id;
	}
	#endregion Pictures
	#region Places
	/// <summary>
	/// The types of geography the system supports
	/// </summary>
	public enum ShapeType {
		/// <summary> 
		/// A lat/long coordinate pair. Places of this type do not contain the radius or shape keys.
		/// </summary>
		point,
		/// <summary> 
		/// The shape is an array of lat/long coordinates used to create a boundary. Places of this type do not contain the radius key.
		/// </summary>
		polygon,
		/// <summary> 
		/// Defined as a radius around a lat/long pair used to create a circular boundary. Places of this type do not contain the shape key.
		/// </summary>
		radial,
		/// <summary> 
		/// The shape is an array of two lat/long coordinates defined as north-east and south-west corners. Places of this type do not contain the radius key.
		/// </summary>
		rectangle
	}

	/// <summary> 
	/// Relevant details of a place which was deleted.
	/// </summary>
	public partial class PlaceDeleted {
		/// <summary>
		/// The company to which this place belonged.
		/// </summary>
		public ulong company;
		/// <summary>
		/// Unique identifier of this place.
		/// </summary>
		public ulong id;
	}
	/// <summary> 
	/// A POI (point-of-interest) saved to the system to help determine an asset's real-world position. Depending on the kind, the place will contain different properties. Only radial type places will contain the radius property, and only polygon and rectangle type places will contain the shape array.
	/// </summary>
	public partial class PlaceGeneral {
		/// <summary>
		/// Full street address including province/state, country, and postal/zip code.
		/// </summary>
		/// <remarks>max-length: 200</remarks>
		public string address;
		/// <summary>
		/// General coordinates. Used to place a marker on a map, or a specific door or loading bay for dispatch.
		/// </summary>
		public LatLng anchor;
		/// <summary>
		/// The fill colour given to this place for easy visual identification on the map (given in 24bit hex; #RRGGBB)
		/// </summary>
		/// <remarks>max-length: 22</remarks>
		public string colour;
		/// <summary>
		/// The company to which this POI belongs.
		/// </summary>
		public ulong company;
		/// <summary>
		/// The icon used to display this POI in lists and on the map.
		/// </summary>
		public ulong icon;
		/// <summary>
		/// Unique identifier of this POI.
		/// </summary>
		public ulong id;
		/// <summary>
		/// The kind of geography represented by this POI.
		/// </summary>
		public ShapeType kind;
		/// <summary>
		/// The codified names of labels
		/// </summary>
		public string[] labels;
		/// <summary>
		/// POI's common name instead of street address.
		/// </summary>
		/// <remarks>max-length: 100</remarks>
		public string name;
		/// <summary>
		/// Notes!
		/// </summary>
		public string notes;
		/// <summary>
		/// Images of this POI.
		/// </summary>
		public ulong[] pictures;
		/// <summary>
		/// Boundary threshold (in meters). This key is only present for radial shape types.
		/// </summary>
		public double radius;
		/// <summary>
		/// A custom field used to refer to an external system.
		/// </summary>
		/// <remarks>max-length: 100</remarks>
		public string reference;
		/// <summary>
		/// The geography representing this POI for rectangle and polygon shape types. For radial and point shape types, the shape key is not present. For a rectangle , the array contains the north east and south west corner coordinates. For a polygon , the array lists all coordinates (oriented as counter-clockwise) needed to draw the geofence.
		/// </summary>
		public LatLng[] shape;
		/// <summary>
		/// Object version keys used to validate synchronization for different properties.
		/// v[0]: Properties found in the PlaceGeneral object.
		/// </summary>
		/// <remarks>length: 1</remarks>
		public uint[] v;
	}

	#endregion Places
	#region Providers
	/// <summary>
	/// Event groups used in ProviderAdvanced.data.
	/// </summary>
	public enum ProviderDataGroup {
		/// <summary> 
		/// Dashcam picture and video recording events
		/// </summary>
		CAMERA,
		/// <summary> 
		/// Cellular network information.
		/// </summary>
		CELL,
		/// <summary> 
		/// Task, clock-in/out, paired vehicle and driver information.
		/// </summary>
		DISPATCH,
		/// <summary> 
		/// GPS and location data.
		/// </summary>
		GPS,
		/// <summary> 
		/// Wiring data.
		/// </summary>
		IO,
		/// <summary> 
		/// Provider status and sensor data.
		/// </summary>
		STATUS,
		/// <summary> 
		/// OBD-II and J-Bus data.
		/// </summary>
		VBUS
	}

	/// <summary>
	/// The kinds of values given by providers in ProviderAdvanced.attributes[ProviderDataGroup]:ProviderData.
	/// </summary>
	public enum ProviderDataName {
		/// <summary> 
		/// A virtual accelerometer calculated by the GPS system's positions
		/// </summary>
		ACCELEROMETER,
		/// <summary> 
		/// 
		/// </summary>
		ACK_BYTES_IN,
		/// <summary> 
		/// 
		/// </summary>
		AIR_TEMPERATURE,
		/// <summary> 
		/// Analog voltage reading from wire zero, or external power voltage, or internal battery voltage
		/// </summary>
		ANALOG_INPUT_0,
		/// <summary> 
		/// Analog voltage reading from wire #1
		/// </summary>
		ANALOG_INPUT_1,
		/// <summary> 
		/// Analog voltage reading from wire #10
		/// </summary>
		ANALOG_INPUT_10,
		/// <summary> 
		/// Analog voltage reading from wire #11
		/// </summary>
		ANALOG_INPUT_11,
		/// <summary> 
		/// Analog voltage reading from wire #2
		/// </summary>
		ANALOG_INPUT_2,
		/// <summary> 
		/// Analog voltage reading from wire #3
		/// </summary>
		ANALOG_INPUT_3,
		/// <summary> 
		/// Analog voltage reading from wire #4
		/// </summary>
		ANALOG_INPUT_4,
		/// <summary> 
		/// Analog voltage reading from wire #5
		/// </summary>
		ANALOG_INPUT_5,
		/// <summary> 
		/// Analog voltage reading from wire #6
		/// </summary>
		ANALOG_INPUT_6,
		/// <summary> 
		/// Analog voltage reading from wire #7
		/// </summary>
		ANALOG_INPUT_7,
		/// <summary> 
		/// Analog voltage reading from wire #8
		/// </summary>
		ANALOG_INPUT_8,
		/// <summary> 
		/// Analog voltage reading from wire #9
		/// </summary>
		ANALOG_INPUT_9,
		/// <summary> 
		/// 
		/// </summary>
		ANTI_ICE_RATE,
		/// <summary> 
		/// Identifier of the application communicating with the service(s).
		/// </summary>
		APP_ID,
		/// <summary> 
		/// Software/firmware version information.
		/// </summary>
		APP_VERSION,
		/// <summary> 
		/// 
		/// </summary>
		BILLING,
		/// <summary> 
		/// 
		/// </summary>
		BLAST,
		/// <summary> 
		/// Boot or kernel version information.
		/// </summary>
		BOOT_VERSION,
		/// <summary> 
		/// 
		/// </summary>
		CAMERA_1,
		/// <summary> 
		/// 
		/// </summary>
		CAMERA_2,
		/// <summary> 
		/// 
		/// </summary>
		CAMERA_3,
		/// <summary> 
		/// 
		/// </summary>
		CAMERA_4,
		/// <summary> 
		/// 
		/// </summary>
		CELLUAR_IP_ADDRESS,
		/// <summary> 
		/// 
		/// </summary>
		CELLUAR_ROAMING,
		/// <summary> 
		/// 
		/// </summary>
		CELLUAR_SERVICE,
		/// <summary> 
		/// 
		/// </summary>
		CELLUAR_WAN_IP,
		/// <summary> 
		/// 
		/// </summary>
		CELLULAR_CARRIER,
		/// <summary> 
		/// 
		/// </summary>
		CELLULAR_COMM_STATE,
		/// <summary> 
		/// 
		/// </summary>
		CELLULAR_RSSI,
		/// <summary> 
		/// 
		/// </summary>
		CONFIG_VERSION,
		/// <summary> 
		/// 
		/// </summary>
		CONTROLLER,
		/// <summary> 
		/// 
		/// </summary>
		COURSE,
		/// <summary> 
		/// 
		/// </summary>
		DATA_AGE,
		/// <summary> 
		/// 
		/// </summary>
		DATA_BYTES_IN,
		/// <summary> 
		/// 
		/// </summary>
		DATA_BYTES_OUT,
		/// <summary> 
		/// Digital input wire zero, or external power, or internal battery power
		/// </summary>
		DIGITAL_INPUT_0,
		/// <summary> 
		/// Digital input wire #1
		/// </summary>
		DIGITAL_INPUT_1,
		/// <summary> 
		/// Digital input wire #10
		/// </summary>
		DIGITAL_INPUT_10,
		/// <summary> 
		/// Digital input wire #11
		/// </summary>
		DIGITAL_INPUT_11,
		/// <summary> 
		/// Digital input wire #2
		/// </summary>
		DIGITAL_INPUT_2,
		/// <summary> 
		/// Digital input wire #3
		/// </summary>
		DIGITAL_INPUT_3,
		/// <summary> 
		/// Digital input wire #4
		/// </summary>
		DIGITAL_INPUT_4,
		/// <summary> 
		/// Digital input wire #5
		/// </summary>
		DIGITAL_INPUT_5,
		/// <summary> 
		/// Digital input wire #6
		/// </summary>
		DIGITAL_INPUT_6,
		/// <summary> 
		/// Digital input wire #7
		/// </summary>
		DIGITAL_INPUT_7,
		/// <summary> 
		/// Digital input wire #8
		/// </summary>
		DIGITAL_INPUT_8,
		/// <summary> 
		/// Digital input wire #9
		/// </summary>
		DIGITAL_INPUT_9,
		/// <summary> 
		/// Legacy AirLink digital input wire #2
		/// </summary>
		DIGITAL_INPUT_DTR,
		/// <summary> 
		/// Legacy AirLink digital input wire #1
		/// </summary>
		DIGITAL_INPUT_RTS,
		/// <summary> 
		/// Voltage output wire #0
		/// </summary>
		DIGITAL_OUTPUT_0,
		/// <summary> 
		/// Voltage output wire #1
		/// </summary>
		DIGITAL_OUTPUT_1,
		/// <summary> 
		/// Voltage output wire #10
		/// </summary>
		DIGITAL_OUTPUT_10,
		/// <summary> 
		/// Voltage output wire #99
		/// </summary>
		DIGITAL_OUTPUT_11,
		/// <summary> 
		/// Voltage output wire #2
		/// </summary>
		DIGITAL_OUTPUT_2,
		/// <summary> 
		/// Voltage output wire #3
		/// </summary>
		DIGITAL_OUTPUT_3,
		/// <summary> 
		/// Voltage output wire #4
		/// </summary>
		DIGITAL_OUTPUT_4,
		/// <summary> 
		/// Voltage output wire #5
		/// </summary>
		DIGITAL_OUTPUT_5,
		/// <summary> 
		/// Voltage output wire #6
		/// </summary>
		DIGITAL_OUTPUT_6,
		/// <summary> 
		/// Voltage output wire #7
		/// </summary>
		DIGITAL_OUTPUT_7,
		/// <summary> 
		/// Voltage output wire #8
		/// </summary>
		DIGITAL_OUTPUT_8,
		/// <summary> 
		/// Voltage output wire #9
		/// </summary>
		DIGITAL_OUTPUT_9,
		/// <summary> 
		/// A task is marked as active or "on-route" when the DispatchTask.id is sent.
		/// </summary>
		DISPATCH_TASK,
		/// <summary> 
		/// 
		/// </summary>
		DISTANCE_TRAVELLED_AFTER_MIL_ON,
		/// <summary> 
		/// When the driver changes status to "driving" from another status.
		/// </summary>
		DRIVER_DRIVING,
		/// <summary> 
		/// When the driver logs-in the terminal/vehicle.
		/// </summary>
		DRIVER_LOGIN,
		/// <summary> 
		/// When the driver logs-off the terminal/vehicle.
		/// </summary>
		DRIVER_LOGOFF,
		/// <summary> 
		/// When the driver changes status to "off-duty" from another status.
		/// </summary>
		DRIVER_OFFDUTY,
		/// <summary> 
		/// When the driver changes status to "on-duty" from another status.
		/// </summary>
		DRIVER_ONDUTY,
		/// <summary> 
		/// When the driver changes status to "sleeping" from another status.
		/// </summary>
		DRIVER_SLEEPING,
		/// <summary> 
		/// When the driver chooses a specific vehicle during the log-in process.
		/// </summary>
		DRIVER_VEHICLE,
		/// <summary> 
		/// 
		/// </summary>
		DRY_RATE,
		/// <summary> 
		/// 
		/// </summary>
		ENGINE_HOURS,
		/// <summary> 
		/// 
		/// </summary>
		EQUIVALENT_COMMAND_RATIO,
		/// <summary> 
		/// The device's ESN (Electronic Serial Number).
		/// </summary>
		ESN,
		/// <summary> 
		/// The provider-specific event code.
		/// </summary>
		EVENT_CODE,
		/// <summary> 
		/// The provider-specific event name. See ProviderEventName for more information.
		/// </summary>
		EVENT_NAME,
		/// <summary> 
		/// The time of the event.
		/// </summary>
		EVENT_TIME,
		/// <summary> 
		/// Temperature measured outside of the modem casing.
		/// </summary>
		EXTERNAL_TEMPERATURE,
		/// <summary> 
		/// 
		/// </summary>
		FUEL_TYPE,
		/// <summary> 
		/// ID of violated geofence
		/// </summary>
		GEOFENCE_ID,
		/// <summary> 
		/// Geofence violation types(outside fence violation, inside fence violation)
		/// </summary>
		GEOFENCE_VIOLATION,
		/// <summary> 
		/// Switchable input/output wire zero, or external power, or internal battery
		/// </summary>
		GPIO0,
		/// <summary> 
		/// Switchable input/output wire #1
		/// </summary>
		GPIO1,
		/// <summary> 
		/// Switchable input/output wire #2
		/// </summary>
		GPIO2,
		/// <summary> 
		/// Switchable input/output wire #3
		/// </summary>
		GPIO3,
		/// <summary> 
		/// Switchable input/output wire #4
		/// </summary>
		GPIO4,
		/// <summary> 
		/// Switchable input/output wire #5
		/// </summary>
		GPIO5,
		/// <summary> 
		/// Switchable input/output wire #6
		/// </summary>
		GPIO6,
		/// <summary> 
		/// Switchable input/output wire #7
		/// </summary>
		GPIO7,
		/// <summary> 
		/// Switchable input/output wire #8
		/// </summary>
		GPIO8,
		/// <summary> 
		/// Switchable input/output wire #9
		/// </summary>
		GPIO9,
		/// <summary> 
		/// 
		/// </summary>
		GPR_STATE,
		/// <summary> 
		/// A virtual accelerometer calculated by the GPS system's positions
		/// </summary>
		GPS_ACCELERATION,
		/// <summary> 
		/// The approximate accuracy of the GPS fix
		/// </summary>
		GPS_ACCURACY,
		/// <summary> 
		/// Altitude above sea-level
		/// </summary>
		GPS_ALTITUDE,
		/// <summary> 
		/// GPS antenna status could be related to power or unplugged states
		/// </summary>
		GPS_ANTENNA_STATUS,
		/// <summary> 
		/// A virtual accelerometer calculated by the GPS system's positions
		/// </summary>
		GPS_DECELERATION,
		/// <summary> 
		/// Distance measured between two GPS fixes
		/// </summary>
		GPS_DISTANCE,
		/// <summary> 
		/// Status or validity of the GPS fix
		/// </summary>
		GPS_FIX_STATUS,
		/// <summary> 
		/// Date/time stamp of the most recent GPS fix
		/// </summary>
		GPS_FIX_TIME,
		/// <summary> 
		/// GPS chip information.
		/// </summary>
		GPS_HARDWARE,
		/// <summary> 
		/// Horizontal dilution of precision for 3D fixes
		/// </summary>
		GPS_HDOP,
		/// <summary> 
		/// Direction of travel
		/// </summary>
		GPS_HEADING,
		/// <summary> 
		/// Latitude in decimal degrees
		/// </summary>
		GPS_LATITUDE,
		/// <summary> 
		/// Longitude in decimal degrees
		/// </summary>
		GPS_LONGITUDE,
		/// <summary> 
		/// Date/time stamp from when the device lost GPS signal
		/// </summary>
		GPS_LOST_TIME,
		/// <summary> 
		/// A virtual odometer accumulated by the GPS system's positions
		/// </summary>
		GPS_ODOMETER,
		/// <summary> 
		/// The number of satellites used to obtain the GPS fix
		/// </summary>
		GPS_SATELLITES,
		/// <summary> 
		/// Speed measured by GPS fix differentiation
		/// </summary>
		GPS_SPEED,
		/// <summary> 
		/// 
		/// </summary>
		GSM_STATE,
		/// <summary> 
		/// Device model information.
		/// </summary>
		HARDWARE_VERSION,
		/// <summary> 
		/// 
		/// </summary>
		HOS_DRIVER,
		/// <summary> 
		/// 
		/// </summary>
		HOS_IS_CODRIVER,
		/// <summary> 
		/// 
		/// </summary>
		HOS_STATUS,
		/// <summary> 
		/// 
		/// </summary>
		HOS_VEHICLE,
		/// <summary> 
		/// 
		/// </summary>
		HOS_VIOLATION,
		/// <summary> 
		/// 
		/// </summary>
		HOURS_DISCONNECTED_COUNTER,
		/// <summary> 
		/// 
		/// </summary>
		HOURS_ON_COUNTER,
		/// <summary> 
		/// The device's ICCID (Integrated Circuit Card ID)
		/// </summary>
		ICCID,
		/// <summary> 
		/// The device's IMEI (International Mobile Station Equipment Identity).
		/// </summary>
		IMEI,
		/// <summary> 
		/// The device's IMSI (International Mobile Subscriber Identity).
		/// </summary>
		IMSI,
		/// <summary> 
		/// Temperature measured on the mainboard, or inside the modem casing.
		/// </summary>
		INTERNAL_TEMPERATURE,
		/// <summary> 
		/// 
		/// </summary>
		J1708_ACCELERATOR_PEDAL_POSITION,
		/// <summary> 
		/// 
		/// </summary>
		J1708_BATTERY_VOLTAGE,
		/// <summary> 
		/// 
		/// </summary>
		J1708_DIAGNOSTIC_CODE,
		/// <summary> 
		/// 
		/// </summary>
		J1708_ENGINE_AVERAGE_FUEL_ECONOMY,
		/// <summary> 
		/// 
		/// </summary>
		J1708_ENGINE_COOLANT_PRESSURE,
		/// <summary> 
		/// 
		/// </summary>
		J1708_ENGINE_COOLANT_TEMP,
		/// <summary> 
		/// 
		/// </summary>
		J1708_ENGINE_OIL_PRESSURE,
		/// <summary> 
		/// 
		/// </summary>
		J1708_ENGINE_OIL_TEMP,
		/// <summary> 
		/// 
		/// </summary>
		J1708_ENGINE_SPEED,
		/// <summary> 
		/// 
		/// </summary>
		J1708_FUEL_RATE,
		/// <summary> 
		/// Odometer reading from J-Bus 1708
		/// </summary>
		J1708_ODOMETER,
		/// <summary> 
		/// 
		/// </summary>
		J1708_SEATBELT,
		/// <summary> 
		/// 
		/// </summary>
		J1708_STATUS,
		/// <summary> 
		/// 
		/// </summary>
		J1708_TOTAL_ENGINE_HOURS,
		/// <summary> 
		/// 
		/// </summary>
		J1708_TOTAL_FUEL,
		/// <summary> 
		/// 
		/// </summary>
		J1708_TOTAL_IDLE_FUEL,
		/// <summary> 
		/// 
		/// </summary>
		J1708_TOTAL_IDLE_HOURS,
		/// <summary> 
		/// 
		/// </summary>
		J1708_VEHICLE_SPEED,
		/// <summary> 
		/// 
		/// </summary>
		J1939_ACCELERATOR_PEDAL_POSITION,
		/// <summary> 
		/// 
		/// </summary>
		J1939_BATTERY_VOLTAGE,
		/// <summary> 
		/// 
		/// </summary>
		J1939_DIAGNOSTIC_CODE,
		/// <summary> 
		/// 
		/// </summary>
		J1939_ENGINE_AVERAGE_FUEL_ECONOMY,
		/// <summary> 
		/// 
		/// </summary>
		J1939_ENGINE_COOLANT_PRESSURE,
		/// <summary> 
		/// 
		/// </summary>
		J1939_ENGINE_COOLANT_TEMP,
		/// <summary> 
		/// 
		/// </summary>
		J1939_ENGINE_OIL_PRESSURE,
		/// <summary> 
		/// 
		/// </summary>
		J1939_ENGINE_OIL_TEMP,
		/// <summary> 
		/// 
		/// </summary>
		J1939_ENGINE_SPEED,
		/// <summary> 
		/// 
		/// </summary>
		J1939_FUEL_RATE,
		/// <summary> 
		/// Odometer reading from J-Bus 1939
		/// </summary>
		J1939_ODOMETER,
		/// <summary> 
		/// 
		/// </summary>
		J1939_SEATBELT,
		/// <summary> 
		/// 
		/// </summary>
		J1939_STATUS,
		/// <summary> 
		/// 
		/// </summary>
		J1939_TOTAL_ENGINE_HOURS,
		/// <summary> 
		/// 
		/// </summary>
		J1939_TOTAL_FUEL,
		/// <summary> 
		/// 
		/// </summary>
		J1939_TOTAL_IDLE_FUEL,
		/// <summary> 
		/// 
		/// </summary>
		J1939_TOTAL_IDLE_HOURS,
		/// <summary> 
		/// 
		/// </summary>
		J1939_TRANSMISSION_GEAR,
		/// <summary> 
		/// 
		/// </summary>
		J1939_TURN_SIGNAL,
		/// <summary> 
		/// 
		/// </summary>
		J1939_VEHICLE_SPEED,
		/// <summary> 
		/// 
		/// </summary>
		LAST_CONFIG_UPDATE,
		/// <summary> 
		/// 
		/// </summary>
		LAST_ID_REPORT,
		/// <summary> 
		/// 
		/// </summary>
		LEFT_WIRING,
		/// <summary> 
		/// 
		/// </summary>
		MAGNETIC_VARIATION,
		/// <summary> 
		/// 
		/// </summary>
		MAIN_FIRMWARE_VERSION,
		/// <summary> 
		/// 
		/// </summary>
		MARTERIAL_TYPE,
		/// <summary> 
		/// 
		/// </summary>
		MIL_STATUS,
		/// <summary> 
		/// The device's MIN (Mobile Identification Number).
		/// </summary>
		MIN,
		/// <summary> 
		/// Device model name.
		/// </summary>
		MODEM_MODEL,
		/// <summary> 
		/// Device model name.
		/// </summary>
		MODEM_MODEL_ID,
		/// <summary> 
		/// Device friendly name.
		/// </summary>
		MODEM_NAME,
		/// <summary> 
		/// Motion status determined by the GPS speed and ignition
		/// </summary>
		MOTION_STATUS,
		/// <summary> 
		/// 
		/// </summary>
		NEW_SIM_ID,
		/// <summary> 
		/// 
		/// </summary>
		NUMBER_OF_OTA_TRY,
		/// <summary> 
		/// 
		/// </summary>
		OBDII_AIR_FLOW_RATE,
		/// <summary> 
		/// 
		/// </summary>
		OBDII_AIR_INTAKE_TEMPERATURE,
		/// <summary> 
		/// 
		/// </summary>
		OBDII_AMBIENT_AIR_TEMPERATURE,
		/// <summary> 
		/// 
		/// </summary>
		OBDII_BAROMETRIC_PRESSURE,
		/// <summary> 
		/// 
		/// </summary>
		OBDII_BATTERY_VOLTAGE,
		/// <summary> 
		/// 
		/// </summary>
		OBDII_BOOST_PRESSURE_INTAKE_MANIFOLD_PRESSURE,
		/// <summary> 
		/// 
		/// </summary>
		OBDII_DERIVED_ENGINE_STATE,
		/// <summary> 
		/// 
		/// </summary>
		OBDII_DISTANCE_SINCE_CODE_CLEARED,
		/// <summary> 
		/// 
		/// </summary>
		OBDII_DISTANCE_TRAVELLED_AFTER_MIL_ON,
		/// <summary> 
		/// 
		/// </summary>
		OBDII_DTC,
		/// <summary> 
		/// 
		/// </summary>
		OBDII_DTC_COUNT,
		/// <summary> 
		/// 
		/// </summary>
		OBDII_ENGINE_AVERAGE_FUEL_ECONOMY,
		/// <summary> 
		/// 
		/// </summary>
		OBDII_ENGINE_COOLANT_PRESSURE,
		/// <summary> 
		/// 
		/// </summary>
		OBDII_ENGINE_COOLANT_TEMP,
		/// <summary> 
		/// 
		/// </summary>
		OBDII_ENGINE_LOAD,
		/// <summary> 
		/// 
		/// </summary>
		OBDII_ENGINE_OIL_TEMP,
		/// <summary> 
		/// 
		/// </summary>
		OBDII_ENGINE_SPEED,
		/// <summary> 
		/// 
		/// </summary>
		OBDII_EQUIVALENT_COMMAND_RATIO,
		/// <summary> 
		/// 
		/// </summary>
		OBDII_FIRMWARE_VERSION,
		/// <summary> 
		/// 
		/// </summary>
		OBDII_FUEL_AIR_COMMANDED_EQUIVAILENCE_RATIO,
		/// <summary> 
		/// 
		/// </summary>
		OBDII_FUEL_LEVEL,
		/// <summary> 
		/// 
		/// </summary>
		OBDII_FUEL_LEVEL_REMAINING,
		/// <summary> 
		/// 
		/// </summary>
		OBDII_FUEL_PRESSURE,
		/// <summary> 
		/// 
		/// </summary>
		OBDII_FUEL_RAIL_PRESSURE,
		/// <summary> 
		/// 
		/// </summary>
		OBDII_FUEL_RATE,
		/// <summary> 
		/// 
		/// </summary>
		OBDII_FUEL_TYPE,
		/// <summary> 
		/// 
		/// </summary>
		OBDII_HIGH_BATTERY_VOLTAGE,
		/// <summary> 
		/// 
		/// </summary>
		OBDII_IGNITION_TIMING_ADVANCE,
		/// <summary> 
		/// 
		/// </summary>
		OBDII_INDICATORS,
		/// <summary> 
		/// 
		/// </summary>
		OBDII_INSTANT_FULE_RATE,
		/// <summary> 
		/// 
		/// </summary>
		OBDII_LONG_TERM_FUEL_TRIM_BANK_1,
		/// <summary> 
		/// 
		/// </summary>
		OBDII_LONG_TERM_FUEL_TRIM_BANK_2,
		/// <summary> 
		/// 
		/// </summary>
		OBDII_LOW_BATTERY_VOLTAGE,
		/// <summary> 
		/// Odometer reading from CAN-Bus since last DTC code was cleared
		/// </summary>
		OBDII_MILES_SINCE_LAST_ALERT,
		/// <summary> 
		/// Odometer reading from CAN-Bus
		/// </summary>
		OBDII_ODOMETER,
		/// <summary> 
		/// 
		/// </summary>
		OBDII_OEM_PARAMETERS,
		/// <summary> 
		/// 
		/// </summary>
		OBDII_OEM_PROTOCOL,
		/// <summary> 
		/// 
		/// </summary>
		OBDII_PARAMETERS,
		/// <summary> 
		/// 
		/// </summary>
		OBDII_PROTOCOL,
		/// <summary> 
		/// 
		/// </summary>
		OBDII_SEATBELT,
		/// <summary> 
		/// 
		/// </summary>
		OBDII_SHROT_TERM_FUEL_TRIM_BANK_1,
		/// <summary> 
		/// 
		/// </summary>
		OBDII_SHROT_TERM_FUEL_TRIM_BANK_2,
		/// <summary> 
		/// 
		/// </summary>
		OBDII_THROTTLE_POSITION,
		/// <summary> 
		/// 
		/// </summary>
		OBDII_TOTAL_ENGINE_HOURS,
		/// <summary> 
		/// 
		/// </summary>
		OBDII_TRANSMISSION_GEAR,
		/// <summary> 
		/// 
		/// </summary>
		OBDII_TRIP_FUEL_CONSUMPTION,
		/// <summary> 
		/// 
		/// </summary>
		OBDII_TRIP_ODOMETER,
		/// <summary> 
		/// 
		/// </summary>
		OBDII_TURN_SIGNAL,
		/// <summary> 
		/// 
		/// </summary>
		OBDII_VEHICLE_SPEED,
		/// <summary> 
		/// 
		/// </summary>
		OBDII_WIDEBAND_AIR_FUEL_RATIO,
		/// <summary> 
		/// 
		/// </summary>
		OBDII_YMME,
		/// <summary> 
		/// Operating System name (appropriate for mobile phones powered by Android or iOS).
		/// </summary>
		OS_NAME,
		/// <summary> 
		/// Operating System version information.
		/// </summary>
		OS_VERSION,
		/// <summary> 
		/// 
		/// </summary>
		PASSTHROUGH_USER_MESSAGE,
		/// <summary> 
		/// 
		/// </summary>
		PASSTHROUGH_USER_MESSAGE_ID,
		/// <summary> 
		/// 
		/// </summary>
		PAUSE,
		/// <summary> 
		/// 
		/// </summary>
		PDP_STATE,
		/// <summary> 
		/// 
		/// </summary>
		PERCENT_LOST_GPRS,
		/// <summary> 
		/// Date/time stamp from when the device lost its precise GPS position
		/// </summary>
		PERCENT_LOST_GPS,
		/// <summary> 
		/// 
		/// </summary>
		PERCENT_LOST_GPS_QUALITY_FACTOR,
		/// <summary> 
		/// 
		/// </summary>
		PERCENT_LOST_GSM,
		/// <summary> 
		/// 
		/// </summary>
		PERCENT_LOST_PDP,
		/// <summary> 
		/// Phone number
		/// </summary>
		PHONE_NUMBER,
		/// <summary> 
		/// 
		/// </summary>
		PLOW_PRIMARY,
		/// <summary> 
		/// 
		/// </summary>
		PLOW_SECONDARY,
		/// <summary> 
		/// 
		/// </summary>
		POLICY_FAULT_MESSAGEID,
		/// <summary> 
		/// 
		/// </summary>
		POLICY_FAULT_TEXT,
		/// <summary> 
		/// 
		/// </summary>
		POWER_UP_COUNTER,
		/// <summary> 
		/// 
		/// </summary>
		PREFERED_ROAMING_LIST,
		/// <summary> 
		/// 
		/// </summary>
		PREVIOUS_SIM_ID,
		/// <summary> 
		/// 
		/// </summary>
		PREWET_RATE,
		/// <summary> 
		/// 
		/// </summary>
		RADIO_VERSION,
		/// <summary> 
		/// 
		/// </summary>
		RAP_VERSION,
		/// <summary> 
		/// 
		/// </summary>
		REGISTRATION_ID,
		/// <summary> 
		/// 
		/// </summary>
		RESET_COUNTER,
		/// <summary> 
		/// 
		/// </summary>
		RIGHT_WIRING,
		/// <summary> 
		/// 
		/// </summary>
		ROAD_TEMPERATURE,
		/// <summary> 
		/// The device's secondary IMEI.
		/// </summary>
		SECONDARY_IMEI,
		/// <summary> 
		/// 
		/// </summary>
		SEQUENCE_NUMBER,
		/// <summary> 
		/// 
		/// </summary>
		SEQUENCE_NUMBER_FIRST,
		/// <summary> 
		/// 
		/// </summary>
		SEQUENCE_NUMBER_LAST,
		/// <summary> 
		/// A serial not used as ESN, IMEI, or similar.
		/// </summary>
		SERIAL_NUMBER,
		/// <summary> 
		/// 
		/// </summary>
		SERVICE_FAULT_MESSAGEID,
		/// <summary> 
		/// 
		/// </summary>
		SERVICE_FAULT_TEXT,
		/// <summary> 
		/// 
		/// </summary>
		SERVICE_RESPONSE,
		/// <summary> 
		/// 
		/// </summary>
		SMS_MSG_IN_COUNTER,
		/// <summary> 
		/// 
		/// </summary>
		SMS_MSG_OUT_COUNTER,
		/// <summary> 
		/// 
		/// </summary>
		SMS_SPAM_MSG_IN_COUNTER,
		/// <summary> 
		/// 
		/// </summary>
		SOURCE,
		/// <summary> 
		/// 
		/// </summary>
		SPINNER_RATE,
		/// <summary> 
		/// 
		/// </summary>
		SPREADER_STATUS_BLAST,
		/// <summary> 
		/// 
		/// </summary>
		SPREADER_STATUS_COMMUNICATION,
		/// <summary> 
		/// 
		/// </summary>
		SPREADER_STATUS_LIQUID,
		/// <summary> 
		/// 
		/// </summary>
		SPREADER_STATUS_PREWET,
		/// <summary> 
		/// 
		/// </summary>
		SPREADER_STATUS_SOLID,
		/// <summary> 
		/// Booting
		/// </summary>
		STATUS_POWERUP,
		/// <summary> 
		/// Low power mode
		/// </summary>
		STATUS_SLEEP,
		/// <summary> 
		/// High power mode
		/// </summary>
		STATUS_WAKEUP,
		/// <summary> 
		/// 
		/// </summary>
		TAG_COUNT,
		/// <summary> 
		/// 
		/// </summary>
		TAG_LIST,
		/// <summary> 
		/// 
		/// </summary>
		TEMPERATURE_SENSOR_1,
		/// <summary> 
		/// 
		/// </summary>
		TEMPERATURE_SENSOR_2,
		/// <summary> 
		/// 
		/// </summary>
		TEMPERATURE_SENSOR_3,
		/// <summary> 
		/// 
		/// </summary>
		TEMPERATURE_SENSOR_4,
		/// <summary> 
		/// 
		/// </summary>
		TERMINAL_STATUS,
		/// <summary> 
		/// 
		/// </summary>
		TOTAL_LIQUID,
		/// <summary> 
		/// 
		/// </summary>
		TOTAL_PREWET,
		/// <summary> 
		/// 
		/// </summary>
		TOTAL_SOLID,
		/// <summary> 
		/// 
		/// </summary>
		UNIT_STATUS,
		/// <summary> 
		/// 
		/// </summary>
		VBUS_ACCELERATOR_PEDAL_POSITION,
		/// <summary> 
		/// 
		/// </summary>
		VBUS_BRAKE_SWITCH_STATUS,
		/// <summary> 
		/// 
		/// </summary>
		VBUS_TOTAL_FUEL_USED,
		/// <summary> 
		/// 
		/// </summary>
		VIN
	}

	/// <summary>
	/// Suggested units for ProviderData attributes.
	/// </summary>
	public enum ProviderDataUnits {
		/// <summary> 
		/// Amperage
		/// </summary>
		A,
		/// <summary> 
		/// Speed in centimetres per second
		/// </summary>
		cmps,
		/// <summary> 
		/// Centimetres per second squared. Used for accelerometers.
		/// </summary>
		cmps2,
		/// <summary> 
		/// Degrees Celsius. Used for temperature
		/// </summary>
		dC,
		/// <summary> 
		/// Used for heading/bearing, latitude, and longitude
		/// </summary>
		degree,
		/// <summary> 
		/// Degrees Fahrenheit. Used for temperature
		/// </summary>
		dF,
		/// <summary> 
		/// Degrees Kelvin. Used for temperature
		/// </summary>
		dK,
		/// <summary> 
		/// Volume in US gallons
		/// </summary>
		gal,
		/// <summary> 
		/// G-force. Used for accelerometers.
		/// </summary>
		Gf,
		/// <summary> 
		/// Weight by gram
		/// </summary>
		gram,
		/// <summary> 
		/// Used for times.
		/// </summary>
		hour,
		/// <summary> 
		/// Weight by kilogram
		/// </summary>
		Kg,
		/// <summary> 
		/// Dry rate in kilogram per kilometer
		/// </summary>
		kgpkm,
		/// <summary> 
		/// Distance
		/// </summary>
		kilometer,
		/// <summary> 
		/// Speed in nautical miles per hour
		/// </summary>
		kn,
		/// <summary> 
		/// Speed in nautical miles per second
		/// </summary>
		knps,
		/// <summary> 
		/// Speed in kilometres per hour
		/// </summary>
		kph,
		/// <summary> 
		/// Volume in litres
		/// </summary>
		L,
		/// <summary> 
		/// Fuel consumption in litres per hour
		/// </summary>
		Lph,
		/// <summary> 
		/// Fuel economy in litres per 100 kilometres
		/// </summary>
		Lphkm,
		/// <summary> 
		/// Prew and anti ice rate in liter per kilometer
		/// </summary>
		lpkm,
		/// <summary> 
		/// Distance
		/// </summary>
		meter,
		/// <summary> 
		/// Milli-G-force (thousandths). Used for accelerometers.
		/// </summary>
		mGf,
		/// <summary> 
		/// Distance
		/// </summary>
		mile,
		/// <summary> 
		/// Used for times.
		/// </summary>
		minute,
		/// <summary> 
		/// Speed in miles per second
		/// </summary>
		mips,
		/// <summary> 
		/// Volume in millilitres
		/// </summary>
		mL,
		/// <summary> 
		/// Fuel consumption in millilitres per hour
		/// </summary>
		mLph,
		/// <summary> 
		/// Fuel economy in miles per gallon
		/// </summary>
		mpg,
		/// <summary> 
		/// Speed in miles per hour
		/// </summary>
		mph,
		/// <summary> 
		/// Voltage in millivolts
		/// </summary>
		mV,
		/// <summary> 
		/// Percentage (%)
		/// </summary>
		percent,
		/// <summary> 
		/// Engine speed
		/// </summary>
		RPM,
		/// <summary> 
		/// Used for times.
		/// </summary>
		second,
		/// <summary> 
		/// Voltage
		/// </summary>
		V
	}

	/// <summary>
	/// The kinds of event messages.
	/// </summary>
	public enum ProviderEventName {
		/// <summary> 
		/// Hard-acceleration detected
		/// </summary>
		ACCELERATION,
		/// <summary> 
		/// Hard-braking detected
		/// </summary>
		DECELERATION,
		/// <summary> 
		/// GPS signal re-acquired.
		/// </summary>
		GPS_FOUND,
		/// <summary> 
		/// GPS fix was lost.
		/// </summary>
		GPS_LOST,
		/// <summary> 
		/// Hard-cornering detected
		/// </summary>
		HARSH_TURN,
		/// <summary> 
		/// Gen-X iButton clock-in
		/// </summary>
		IB_IN,
		/// <summary> 
		/// Gen-X iButton clock-out
		/// </summary>
		IB_OUT,
		/// <summary> 
		/// Vehicle is stopped and the ignition is on.
		/// </summary>
		IDLING,
		/// <summary> 
		/// Vehicle's ignition turned off.
		/// </summary>
		IGNITION_OFF,
		/// <summary> 
		/// Vehicle's ignition turned on.
		/// </summary>
		IGNITION_ON,
		/// <summary> 
		/// Has entered a designated region or geofence
		/// </summary>
		IN_REGION,
		/// <summary> 
		/// Device is in motion
		/// </summary>
		MOVING,
		/// <summary> 
		/// Has left a designated region or geofence
		/// </summary>
		OUT_REGION,
		/// <summary> 
		/// Vehicle has stopped and engaged parking brake or the transmission was set to "P".
		/// </summary>
		PARK,
		/// <summary> 
		/// Regular interval report.
		/// </summary>
		PERIODIC,
		/// <summary> 
		/// External power was connected.
		/// </summary>
		POWER_CONNECTED,
		/// <summary> 
		/// External power was disconnected.
		/// </summary>
		POWER_DISCONNECTED,
		/// <summary> 
		/// Modem is powering off or shutting down.
		/// </summary>
		POWER_OFF,
		/// <summary> 
		/// Modem has completed its power-on or boot routine.
		/// </summary>
		POWER_UP,
		/// <summary> 
		/// Modem is shutting down immediately.
		/// </summary>
		SHORT_POWER_OUT,
		/// <summary> 
		/// Modem is going in to low-power state or sleep mode.
		/// </summary>
		SLEEP,
		/// <summary> 
		/// Device is speeding
		/// </summary>
		SPEEDING,
		/// <summary> 
		/// Device stopped speeding
		/// </summary>
		SPEEDING_STOP,
		/// <summary> 
		/// Device has stopped moving
		/// </summary>
		STOP_MOVING,
		/// <summary> 
		/// Tamper detection sensor activated
		/// </summary>
		TAMPER_DETECT,
		/// <summary> 
		/// Unknown or undefined event name.
		/// </summary>
		UNDEFINED,
		/// <summary> 
		/// VIN has been retreived from VBus
		/// </summary>
		VIN_FOUND,
		/// <summary> 
		/// Modem is coming in to normal power consumption state, or wake mode.
		/// </summary>
		WAKE_UP
	}

	/// <summary>
	/// Protocols supported by the system.
	/// </summary>
	public enum ProviderType {
		/// <summary> 
		/// Sierra Wireless AirLink RAP protocol
		/// </summary>
		airlink,
		/// <summary> 
		/// Sixnet BlueTree BEP protocol
		/// </summary>
		bluetree,
		/// <summary> 
		/// TachWest DataTrans protocol
		/// </summary>
		datatrans,
		/// <summary> 
		/// Gen-X modem protocol
		/// </summary>
		genx,
		/// <summary> 
		/// Fleet Freedom JSON protocol
		/// </summary>
		json,
		/// <summary> 
		/// Bell Mobility LBS
		/// </summary>
		lbs,
		/// <summary> 
		/// CalAmp LMU series protocol
		/// </summary>
		lmu,
		/// <summary> 
		/// Trak iT Wireless Mobile App
		/// </summary>
		mobile,
		/// <summary> 
		/// SmartWitness dashcam formats
		/// </summary>
		smartwitness,
		/// <summary> 
		/// Novotel Enfora SpiderAT protocol
		/// </summary>
		spiderAT,
		/// <summary> 
		/// Novotel Enfora SpiderMT protocol
		/// </summary>
		spiderMT,
		/// <summary> 
		/// Certified Tracking protocol
		/// </summary>
		titan,
		/// <summary> 
		/// CalAmp TTU series protocol
		/// </summary>
		ttu,
		/// <summary> 
		/// Your guess is as good as mine. It should never be this.
		/// </summary>
		unknown,
		/// <summary> 
		/// Xirgo modem protocol
		/// </summary>
		xirgo
	}

	/// <summary>
	/// Progress lifetime of changing the on-board information of a remote device.
	/// </summary>
	public enum ProvisioningStatus {
		/// <summary> 
		/// Sending of the new configuration was halted by a user.
		/// </summary>
		cancelled,
		/// <summary> 
		/// New configuration successfully sent to provider.
		/// </summary>
		completed,
		/// <summary> 
		/// The new configuration has been created, but not yet sent to the provider.
		/// </summary>
		created,
		/// <summary> 
		/// Something went wrong while trying to send configuration.
		/// </summary>
		error,
		/// <summary> 
		/// Currently sending configuration over-the-air to the provider.
		/// </summary>
		inProgress,
		/// <summary> 
		/// The maximum number of retries were attempted before giving up.
		/// </summary>
		maxRetries,
		/// <summary> 
		/// A message was sent to the provider asking it to check in.
		/// </summary>
		otaSent,
		/// <summary> 
		/// Only a partial configuration was sent to the provider.
		/// </summary>
		partial,
		/// <summary> 
		/// Provider is notified of new configuration, but has not yet checked in.
		/// </summary>
		pending,
		/// <summary> 
		/// Your guess is as good as mine. It should never be this.
		/// </summary>
		unknown
	}

	/// <summary> 
	/// A geofence defined by a centre coordinate and a threshold value to indicate the boundary around that point.
	/// </summary>
	public partial class CircularGeofence {
		/// <summary>
		/// The maximum number of unique geofences supported by the device.
		/// </summary>
		public uint maxGeofenceCount;
		/// <summary>
		/// The largest possible radius for this geofence.
		/// </summary>
		public uint maxRadius;
		/// <summary>
		/// The smallest possible radius for this geofence.
		/// </summary>
		public uint minRadius;
		/// <summary>
		/// The supported shape of geofence.
		/// </summary>
		public ShapeType type;
	}

	/// <summary> 
	/// An abstract class used as a base for all Geofence type classes.
	/// </summary>
	public partial class GeofenceType {
		/// <summary>
		/// The maximum number of unique geofences supported by the device.
		/// </summary>
		public uint maxGeofenceCount;
		/// <summary>
		/// The supported shape of geofence.
		/// </summary>
		public ShapeType type;
	}

	/// <summary> 
	/// This is a point and not a geofence, so I don't know why this is defined.
	/// </summary>
	public partial class PointGeofence {
		/// <summary>
		/// The maximum number of unique geofences supported by the device.
		/// </summary>
		public uint maxGeofenceCount;
		/// <summary>
		/// The supported shape of geofence.
		/// </summary>
		public ShapeType type;
	}

	/// <summary> 
	/// A geofence whose boundary is defined by a non-overlapping series of coordinates.
	/// </summary>
	public partial class PolygonGeofence {
		/// <summary>
		/// The maximum number of unique geofences supported by the device.
		/// </summary>
		public uint maxGeofenceCount;
		/// <summary>
		/// The maximum number of vertices supported by the device.
		/// </summary>
		public uint maxVertices;
		/// <summary>
		/// The supported shape of geofence.
		/// </summary>
		public ShapeType type;
	}

	/// <summary> 
	/// A device or service which provides data to the system from the field.
	/// </summary>
	public partial class Provider {
		/// <summary>
		/// The asset for which this device provides field data.
		/// </summary>
		public ulong? asset;
		/// <summary>
		/// a list of often changing values like latitude, longitude, speed, wiring state, VBus information, etc...
		/// </summary>
		public Dictionary<string, Dictionary<string, ProviderData>> attributes;
		/// <summary>
		/// The company to which this device belongs.
		/// </summary>
		public ulong company;
		/// <summary>
		/// The provider's current (or pending) configuration profile.
		/// </summary>
		public ulong configuration;
		/// <summary>
		/// The firmware/application version number.
		/// </summary>
		/// <remarks>max-length: 100</remarks>
		public string firmware;
		/// <summary>
		/// The system's progress of updating the device's firmware/application.
		/// </summary>
		public ProvisioningStatus firmwareStatus;
		/// <summary>
		/// A timestamp from when the geofence list was successfully updated on the provider.
		/// </summary>
		public DateTime? geofenceLast;
		/// <summary>
		/// The system's progress of updating the device's on-board geofence definitions.
		/// </summary>
		public ProvisioningStatus geofenceStatus;
		/// <summary>
		/// Unique identifier of this device.
		/// </summary>
		/// <remarks>max-length: 50</remarks>
		public string id;
		/// <summary>
		/// A list of read-only values about the device like IMEI, ESN, firmware version, hardware revision, etc...
		/// </summary>
		public Dictionary<string, string> information;
		/// <summary>
		/// The kind of communication protocol this device uses.
		/// </summary>
		public ProviderType kind;
		/// <summary>
		/// A timestamp from when the provider last checked for a new script or new geofences.
		/// </summary>
		public DateTime? lastCheckIn;
		/// <summary>
		/// The last IP address of the device.
		/// </summary>
		public ipv4 lastIP;
		/// <summary>
		/// A nickname given to the device/hardware.
		/// </summary>
		/// <remarks>max-length: 100</remarks>
		public string name;
		/// <summary>
		/// Notes!
		/// </summary>
		public string notes;
		/// <summary>
		/// The password programmed on the device used to ensure the system is the only client authorized to make changes.
		/// </summary>
		/// <remarks>max-length: 50</remarks>
		public string password;
		/// <summary>
		/// The phone number of this device.
		/// </summary>
		public ulong? phoneNumber;
		/// <summary>
		/// The short-name of the kind of PND attached to this device. Leave blank if none.
		/// </summary>
		/// <remarks>max-length: 50</remarks>
		public string pnd;
		/// <summary>
		/// A timestamp from when the script successfully updated on the provider.
		/// </summary>
		public DateTime? scriptLast;
		/// <summary>
		/// The system's progress of updating the device's programming.
		/// </summary>
		public ProvisioningStatus scriptStatus;
		/// <summary>
		/// Object version keys used to validate synchronization for different properties.
		/// v[0]: Properties found in the ProviderGeneral object.
		/// v[1]: Properties found in the ProviderAdvanced object.
		/// </summary>
		/// <remarks>length: 2</remarks>
		public uint[] v;
	}

	/// <summary> 
	/// Device/hardware information reported from the field.
	/// </summary>
	public partial class ProviderAdvanced {
		/// <summary>
		/// a list of often changing values like latitude, longitude, speed, wiring state, VBus information, etc...
		/// </summary>
		public Dictionary<string, Dictionary<string, ProviderData>> attributes;
		/// <summary>
		/// The company to which this device belongs.
		/// </summary>
		public ulong company;
		/// <summary>
		/// Unique identifier of this device.
		/// </summary>
		/// <remarks>max-length: 50</remarks>
		public string id;
		/// <summary>
		/// The last IP address of the device.
		/// </summary>
		public ipv4 lastIP;
		/// <summary>
		/// Object version keys used to validate synchronization for all object properties.
		/// </summary>
		public uint[] v;
	}

	/// <summary> 
	/// The configured logic loaded onto the provider over-the-air to control it's reporting schedule and behaviour.
	/// </summary>
	public partial class ProviderConfiguration {
		/// <summary>
		/// The company to which this configuration belongs.
		/// </summary>
		public ulong company;
		/// <summary>
		/// List of Places loaded directly onto the provider.
		/// </summary>
		public ulong[] geofences;
		/// <summary>
		/// Unique identifier of this configuration.
		/// </summary>
		public ulong id;
		/// <summary>
		/// The nickname given to this configuration
		/// </summary>
		/// <remarks>max-length: 100</remarks>
		public string name;
		/// <summary>
		/// Simple details about how the providers are expected to behave.
		/// </summary>
		public string notes;
		/// <summary>
		/// The list of defined variables given to the logic type's options pairs for the logic type requires.
		/// </summary>
		public Dictionary<string, object> scriptParameters;
		/// <summary>
		/// The logic type which this configuration implements.
		/// </summary>
		public ulong type;
		/// <summary>
		/// Object version keys used to validate synchronization for all object properties.
		/// </summary>
		public uint[] v;
	}

	/// <summary> 
	/// Relevant details of a configuration which was deleted.
	/// </summary>
	public partial class ProviderConfigurationDeleted {
		/// <summary>
		/// The company to which this configuration belonged.
		/// </summary>
		public ulong company;
		/// <summary>
		/// Unique identifier of this configuration.
		/// </summary>
		public ulong id;
	}
	/// <summary> 
	/// This tree-like structure is given to the script processor for the device type so that the device can follow a program.
	/// </summary>
	public partial class ProviderConfigurationNode {
		/// <summary>
		/// Unique identifier of the value being mapped.
		/// </summary>
		public string id;
		/// <summary>
		/// Indicates that this configuration is an advanced property and should only be set by someone who knows what they're doing.
		/// </summary>
		public bool isAdvanced;
		/// <summary>
		/// The maximum possible value for this confugration node.
		/// </summary>
		public object max;
		/// <summary>
		/// The minimum possible value for this confugration node.
		/// </summary>
		public object min;
		/// <summary>
		/// Child configuration nodes.
		/// </summary>
		public Dictionary<string, ProviderConfigurationNode> nodes;
		/// <summary>
		/// Description of what this configuration does when mapped to a device.
		/// </summary>
		public string notes;
		/// <summary>
		/// Type hint used by the script processor to help format the value.
		/// </summary>
		public string type;
		/// <summary>
		/// Unit hint used to help the script processor format the value.
		/// </summary>
		public ProviderDataUnits unit;
		/// <summary>
		/// The value being set.
		/// </summary>
		public object value;
	}

	/// <summary> 
	/// This read-only class describes a type of logic applied to a provider. ProviderConfigurationTypes are used to help define a ProviderConfiguration.
	/// </summary>
	public partial class ProviderConfigurationType {
		/// <summary>
		/// A list of supported types of geofences which can be programmed directly onto the device.
		/// </summary>
		public GeofenceType[] geofenceTypes;
		/// <summary>
		/// Unique identifier.
		/// </summary>
		public ulong id;
		/// <summary>
		/// The maximum number of geofences that can be programmed onto a device. This number changes based on device make and model, and can also change based on the supported geofence types.
		/// </summary>
		public uint maxGeofenceCount;
		/// <summary>
		/// The minimum number of geofences that need to be programmed onto the device. This value is almost always zero.
		/// </summary>
		public uint minGeofenceCount;
		/// <summary>
		/// Name of the configuration type.
		/// </summary>
		/// <remarks>max-length: 100</remarks>
		public string name;
		/// <summary>
		/// Notes regarding the use of this configuration.
		/// </summary>
		public string notes;
		/// <summary>
		/// The applicable type of provider for which this configuration type can be created.
		/// </summary>
		public ProviderType providerType;
		/// <summary>
		/// A tree-structure of configurations required (or optionally available) for programming a device.
		/// </summary>
		public Dictionary<string, ProviderConfigurationNode> scriptOptions;
		/// <summary>
		/// Object version keys used to validate synchronization for all object properties.
		/// </summary>
		public uint[] v;
	}

	/// <summary> 
	/// A fragment of data given by a device.
	/// </summary>
	public partial class ProviderData {
		/// <summary>
		/// Date/time stamp from when the device recorded (or reported) the data.
		/// </summary>
		public DateTime dts;
		/// <summary>
		/// The relevant unit for the data provided like Km/h, degrees, volts, RPM, etc...
		/// </summary>
		public string unit;
		/// <summary>
		/// The value of the data given like true, 17.3, "asdf", etc...
		/// </summary>
		public object value;
	}

	/// <summary> 
	/// Relevant details of a device which was deleted.
	/// </summary>
	public partial class ProviderDeleted {
		/// <summary>
		/// The company to which this device belonged.
		/// </summary>
		public ulong company;
		/// <summary>
		/// Unique identifier of this device.
		/// </summary>
		public ulong id;
	}

	/// <summary> 
	/// Device/hardware information and configuration.
	/// </summary>
	public partial class ProviderGeneral {
		/// <summary>
		/// The asset for which this device provides field data.
		/// </summary>
		public ulong? asset;
		/// <summary>
		/// The company to which this device belongs.
		/// </summary>
		public ulong company;
		/// <summary>
		/// The provider's current (or pending) configuration profile.
		/// </summary>
		public ulong configuration;
		/// <summary>
		/// The firmware/application version number.
		/// </summary>
		/// <remarks>max-length: 100</remarks>
		public string firmware;
		/// <summary>
		/// The system's progress of updating the device's firmware/application.
		/// </summary>
		public ProvisioningStatus firmwareStatus;
		/// <summary>
		/// A timestamp from when the geofence list was successfully updated on the provider.
		/// </summary>
		public DateTime? geofenceLast;
		/// <summary>
		/// The system's progress of updating the device's on-board geofence definitions.
		/// </summary>
		public ProvisioningStatus geofenceStatus;
		/// <summary>
		/// Unique identifier of this device.
		/// </summary>
		/// <remarks>max-length: 50</remarks>
		public string id;
		/// <summary>
		/// A list of read-only values about the device like IMEI, ESN, firmware version, hardware revision, etc...
		/// </summary>
		public Dictionary<string, string> information;
		/// <summary>
		/// The kind of communication protocol this device uses.
		/// </summary>
		public ProviderType kind;
		/// <summary>
		/// A timestamp from when the provider last checked for a new script or new geofences.
		/// </summary>
		public DateTime? lastCheckIn;
		/// <summary>
		/// A nickname given to the device/hardware.
		/// </summary>
		/// <remarks>max-length: 100</remarks>
		public string name;
		/// <summary>
		/// Notes!
		/// </summary>
		public string notes;
		/// <summary>
		/// The password programmed on the device used to ensure the system is the only client authorized to make changes.
		/// </summary>
		/// <remarks>max-length: 50</remarks>
		public string password;
		/// <summary>
		/// The phone number of this device.
		/// </summary>
		public ulong? phoneNumber;
		/// <summary>
		/// The short-name of the kind of PND attached to this device. Leave blank if none.
		/// </summary>
		/// <remarks>max-length: 50</remarks>
		public string pnd;
		/// <summary>
		/// A timestamp from when the script successfully updated on the provider.
		/// </summary>
		public DateTime? scriptLast;
		/// <summary>
		/// The system's progress of updating the device's programming.
		/// </summary>
		public ProvisioningStatus scriptStatus;
		/// <summary>
		/// Object version keys used to validate synchronization for all object properties.
		/// </summary>
		public uint[] v;
	}

	/// <summary> 
	/// The temporary reference to a device whose ownership is pending.
	/// </summary>
	public partial class ProviderRegistration {
		/// <summary>
		/// The Asset for which this device will provide data.
		/// </summary>
		public ulong? asset;
		/// <summary>
		/// A unique six digit code.
		/// </summary>
		public string code;
		/// <summary>
		/// The company to which the device will belong.
		/// </summary>
		public ulong company;
		/// <summary>
		/// Date/time stamp of when this registration ended successfully.
		/// </summary>
		public DateTime? completed;
		/// <summary>
		/// The predefined configuration this device will use.
		/// </summary>
		public ulong config;
		/// <summary>
		/// The unique identifier of the device that completed this registration.
		/// </summary>
		/// <remarks>max-length: 50</remarks>
		public string identifier;
		/// <summary>
		/// The kind of protocol this device supports.
		/// </summary>
		public ProviderType kind;
		/// <summary>
		/// Date/time stamp of when this registration began.
		/// </summary>
		public DateTime since;
		/// <summary>
		/// The unique identifier the user who generated this registration.
		/// </summary>
		/// <remarks>max-length: 254</remarks>
		public email user;
	}

	/// <summary> 
	/// A geofence whose boundary is a "rectangle" defined by corner coordinates.
	/// </summary>
	public partial class RectangleGeofence {
		/// <summary>
		/// The maximum number of unique geofences supported by the device.
		/// </summary>
		public uint maxGeofenceCount;
		/// <summary>
		/// The smallest possible diameter for this geofence.
		/// </summary>
		public uint maxLength;
		/// <summary>
		/// The smallest possible diameter for this geofence.
		/// </summary>
		public uint maxWidth;
		/// <summary>
		/// The supported shape of geofence.
		/// </summary>
		public ShapeType type;
	}

	#endregion Providers
	#region Real-time Analytics
	/// <summary>
	/// The kinds of parameters required and optional to build Report Results
	/// </summary>
	public enum AnalyticParameterType {
		/// <summary> 
		/// The minimum distance travelled in a Summary Instance before it is included in the results. Supplied as a number of meters.
		/// </summary>
		distance,
		/// <summary> 
		/// The minimum amount of time for a Summary nice before it is included in the results. Supplied as a duration string.
		/// </summary>
		duration,
		/// <summary> 
		/// The maximum amount of time in a Summary Instance before it is split into two and the latter marked as an exception in the results. Supplied as a duration string.
		/// </summary>
		prolonged,
		/// <summary> 
		/// The maximum distance travelled in a Summary Instance before it is split into two and the latter marked as an exception in the results. Supplied as a number of meters.
		/// </summary>
		rubicon,
		/// <summary> 
		/// A list of status tags an Asset must have to be included in the results. Should be a comma separated string of codified tag names.
		/// </summary>
		tags,
		/// <summary> 
		/// Time-span processed on each day. Only data within the time-span is included in the results. The times should be 2 space separated duration strings.
		/// </summary>
		timeOfDay
	}

	/// <summary>
	/// The types an analyses being performed.
	/// </summary>
	public enum AnalyticsType {
		/// <summary> 
		/// All Places visited.
		/// </summary>
		places,
		/// <summary> 
		/// Every geographic region such as cities, provinces/states, and countries.
		/// </summary>
		regions,
		/// <summary> 
		/// Analysis for each Status Tag applied to each asset.
		/// </summary>
		tags,
		/// <summary> 
		/// Life-time of each Task assigned to each asset.
		/// </summary>
		tasks
	}

	/// <summary> 
	/// The targets, filters, and parameters used by an AnalyticRule.
	/// </summary>
	public partial class AnalyticOptions {
		/// <summary>
		/// A targeting expression for limiting results which only include data from Assets interacting with the targeted Places.
		/// </summary>
		public expression filters;
		/// <summary>
		/// A list of parameters to better shape the results.
		/// </summary>
		public AnalyticParameter[] parameters;
		/// <summary>
		/// A targeting expression for including/excluding Assets.
		/// </summary>
		public expression targets;
	}

	/// <summary> 
	/// A configured parameter used by the real-time analytics engine.
	/// </summary>
	public partial class AnalyticParameter {
		/// <summary>
		/// The type of argument.
		/// </summary>
		public AnalyticParameterType kind;
		/// <summary>
		/// The parsed value of the argument. Each type of argument has a different parsing.
		/// </summary>
		public string value;
	}
	/// <summary> 
	/// Rules governing the real-time analysis of a company's assets.
	/// </summary>
	public partial class AnalyticRule {
		/// <summary>
		/// The company to which this analysis belongs.
		/// </summary>
		public ulong company;
		/// <summary>
		/// The fill/background colour of the icon. Should be a hex colour in the format #RRGGBB.
		/// </summary>
		/// <remarks>max-length: 22</remarks>
		public string fill;
		/// <summary>
		/// The name of the symbol for this report.
		/// </summary>
		/// <remarks>max-length: 22</remarks>
		public codified graphic;
		/// <summary>
		/// Unique identifier
		/// </summary>
		public ulong id;
		/// <summary>
		/// The type of analysis done.
		/// </summary>
		public AnalyticsType kind;
		/// <summary>
		/// Name of this report.
		/// </summary>
		/// <remarks>max-length: 100</remarks>
		public string name;
		/// <summary>
		/// Notes about this report.
		/// </summary>
		public string notes;
		/// <summary>
		/// Specified parameters for the report logic, targeted Assets, and filtering Places.
		/// </summary>
		public AnalyticOptions options;
		/// <summary>
		/// Outline and graphic colour. Should be a hex colour in the format #RRGGBB.
		/// </summary>
		/// <remarks>max-length: 22</remarks>
		public string stroke;
		/// <summary>
		/// Object version keys used to validate synchronization for all object properties.
		/// </summary>
		public uint[] v;
	}

	/// <summary> 
	/// Analysis for an asset of a specific type and instance.
	/// </summary>
	public partial class AnalyticSummary {
		/// <summary>
		/// The asset to which this analysis belongs.
		/// </summary>
		public ulong asset;
		/// <summary>
		/// The company to which this analysis belongs.
		/// </summary>
		public ulong company;
		/// <summary>
		/// Denotes that an event has been processed to invalidate the sequence and end the summary.
		/// </summary>
		public bool complete;
		/// <summary>
		/// The distance travelled in kilometres by the asset.
		/// </summary>
		public double distance;
		/// <summary>
		/// The number of events included in the calculation.
		/// </summary>
		public uint eventCount;
		/// <summary>
		/// The first asset state which begins this summary instance.
		/// </summary>
		public AssetAdvanced firstState;
		/// <summary>
		/// Timestamp from the first event in the sequence.
		/// </summary>
		public DateTime firstUtc;
		/// <summary>
		/// Unique identifier
		/// </summary>
		public ulong id;
		/// <summary>
		/// The type of analysis done.
		/// </summary>
		public AnalyticsType kind;
		/// <summary>
		/// The asset state that ended this summary instance.
		/// </summary>
		public AssetAdvanced lastState;
		/// <summary>
		/// Timestamp from the last event in the sequence.
		/// </summary>
		public DateTime lastUtc;
		/// <summary>
		/// An encoded polyline of all the asset's positions in sequence.
		/// </summary>
		public polyline polyline;
		/// <summary>
		/// Code given to this analysis for the asset.
		/// </summary>
		public string state;
		/// <summary>
		/// Object version keys used to validate synchronization for all object properties.
		/// </summary>
		public uint[] v;
	}

	#endregion Real-time Analytics
	#region Reports
	/// <summary>
	/// The kind of reason associated with the range caps for a summary instance.
	/// </summary>
	public enum ReportDataSummaryReason {
		/// <summary> 
		/// The asset started/stopped matching the report filter settings. For example, they left a province or entered a Place.
		/// </summary>
		filterMatch,
		/// <summary> 
		/// If the report starting/ending date range overlaps the actual start of the state.
		/// </summary>
		outsideRange,
		/// <summary> 
		/// The summary instance elapsed a Rubicon or prolonged parameter, and was split into two summary instances.
		/// </summary>
		split,
		/// <summary> 
		/// The asset started/stopped matching the required state. For example, a status tag was added or removed.
		/// </summary>
		stateMatch,
		/// <summary> 
		/// The targeting query starts or stops matching. For example, the Asset's labels were changed.
		/// </summary>
		targeted
	}

	/// <summary>
	/// Drill-down mechanism for highlighting only those places and regions desired in report results.
	/// </summary>
	public enum ReportFilterMode {
		/// <summary> 
		/// Exclude all results except those whose filters match.
		/// </summary>
		exclusive,
		/// <summary> 
		/// Include any results for those whose filters match.
		/// </summary>
		inclusive,
		/// <summary> 
		/// Filtering is not enabled for the report.
		/// </summary>
		none
	}

	/// <summary>
	/// The kinds of parameters required and optional to build Report Results
	/// </summary>
	public enum ReportParameterType {
		/// <summary> 
		/// A list of attribute names an Asset must have to be included in the results. Should be a comma separated string of codified tag names.
		/// </summary>
		attributes,
		/// <summary> 
		/// A choice in the method used to calculate a Summary Instance's values. For an attribute report, the possible values are "instance" where if the attribute is present it is included, and "value" where each attribute's simple value becomes it's own summary instance. For a Tag Summary report, the possible values are "inclusive" where any of the given tags must match, and "exclusive" where all the given tags must match.
		/// </summary>
		collate,
		/// <summary> 
		/// The minimum distance travelled in a Summary Instance before it is included in the results. Supplied as a number of meters.
		/// </summary>
		distance,
		/// <summary> 
		/// The minimum amount of time for a Summary nice before it is included in the results. Supplied as a duration string.
		/// </summary>
		duration,
		/// <summary> 
		/// The ending date/time range for events to be included in the results. The date should be a valid ISO-8601 string.
		/// </summary>
		endDate,
		/// <summary> 
		/// The maximum amount of time in a Summary Instance before it is split into two and the latter marked as an exception in the results. Supplied as a duration string.
		/// </summary>
		prolonged,
		/// <summary> 
		/// The maximum distance travelled in a Summary Instance before it is split into two and the latter marked as an exception in the results. Supplied as a number of meters.
		/// </summary>
		rubicon,
		/// <summary> 
		/// The staring date/time range for events to be included in the results. The date should be a valid ISO-8601 string.
		/// </summary>
		startDate,
		/// <summary> 
		/// A list of status tags an Asset must have to be included in the results. Should be a comma separated string of codified tag names.
		/// </summary>
		tags,
		/// <summary> 
		/// Time-span processed on each day. Only data within the time-span is included in the results. The times should be 2 space separated duration strings.
		/// </summary>
		timeOfDay
	}

	/// <summary>
	/// Specifies how often a Report Template is automatically generates a Report Result.
	/// </summary>
	public enum ReportRecurrenceType {
		/// <summary> 
		/// Yearly at midnight in the local timezone based on the ReportRecurrence.start date.
		/// </summary>
		annually,
		/// <summary> 
		/// Daily at midnight in the local timezone for the previous day based on the ReportRecurrence.weekdays.
		/// </summary>
		daily,
		/// <summary> 
		/// Monthly at midnight in the local timezone for the previous month based on the ReportRecurrence.start date.
		/// </summary>
		monthly,
		/// <summary> 
		/// Runs only once. This type is used for large reports which need to run overnight.
		/// </summary>
		once,
		/// <summary> 
		/// Runs once every three months at midnight in the local timezone for the previous 3 months based on the ReportRecurrence.start date.
		/// </summary>
		quarterly,
		/// <summary> 
		/// Weekly at midnight in the local timezone for the previous 7 days based on the ReportRecurrence.weekday.
		/// </summary>
		weekly
	}

	/// <summary>
	/// The lifetime of building a Report Result.
	/// </summary>
	public enum ReportStatus {
		/// <summary> 
		/// The results are available for retrieval.
		/// </summary>
		completed,
		/// <summary> 
		/// The report results have been requested, but not yet begun processing
		/// </summary>
		created,
		/// <summary> 
		/// There was an error processing the results; see the notes section for a description.
		/// </summary>
		failed,
		/// <summary> 
		/// The report is waiting for required resources to begin running
		/// </summary>
		queued,
		/// <summary> 
		/// The results are currently being processed.
		/// </summary>
		running,
		/// <summary> 
		/// The results have been calculated, and are being saved for review.
		/// </summary>
		saving
	}

	/// <summary>
	/// The type of logic used by the report runner.
	/// </summary>
	public enum ReportType {
		/// <summary> 
		/// Attributes
		/// Summarizes the timeline based on the given attributes and thresholds.
		/// </summary>
		attributes,
		/// <summary> 
		/// Full History
		/// Processes all history for the assets.
		/// </summary>
		full,
		/// <summary> 
		/// Messaging
		/// Processes the log of messages sent to and from the assets.
		/// </summary>
		messages,
		/// <summary> 
		/// Place Summary
		/// Summarizes the timeline based on places visited.
		/// </summary>
		places,
		/// <summary> 
		/// Regions
		/// Summarizes the timeline based on the regions (cities, provinces/states, countries) through which the assets travelled.
		/// </summary>
		regions,
		/// <summary> 
		/// Tag Summary
		/// Summarizes the timeline based on the given tags.
		/// </summary>
		tags,
		/// <summary> 
		/// Task Lifetime
		/// Summarizes the timeline based on the asset's task's lifetimes.
		/// </summary>
		tasks
	}

	/// <summary> 
	/// Asset information used in calculating a summary instance.
	/// </summary>
	public partial class ReportBreakdown {
		/// <summary>
		/// Advanced/detailed information used.
		/// </summary>
		public AssetAdvanced advanced;
		/// <summary>
		/// The asset to which this event data belongs.
		/// </summary>
		public ulong asset;
		/// <summary>
		/// General Asset information.
		/// </summary>
		public AssetGeneral general;
		/// <summary>
		/// Report specific identifier of the event data.
		/// </summary>
		public uint instance;
		/// <summary>
		/// Identifiers of the summary instances that used this event.
		/// </summary>
		public uint[] summaryInstances;
	}

	/// <summary> 
	/// Message information used in this report.
	/// </summary>
	public partial class ReportBreakdownMessage {
		/// <summary>
		/// Advanced/detailed information used.
		/// </summary>
		public AssetAdvanced advanced;
		/// <summary>
		/// The asset to which this event data belongs.
		/// </summary>
		public ulong asset;
		/// <summary>
		/// General Asset information.
		/// </summary>
		public AssetGeneral general;
		/// <summary>
		/// Report specific identifier of the event data.
		/// </summary>
		public uint instance;
		/// <summary>
		/// The Message used.
		/// </summary>
		public Message message;
		/// <summary>
		/// Identifiers of the summary instances that used this event.
		/// </summary>
		public uint[] summaryInstances;
	}

	/// <summary> 
	/// Dispatch Task information used in this report.
	/// </summary>
	public partial class ReportBreakdownTask {
		/// <summary>
		/// Advanced/detailed information used.
		/// </summary>
		public AssetAdvanced advanced;
		/// <summary>
		/// The asset to which this event data belongs.
		/// </summary>
		public ulong asset;
		/// <summary>
		/// General Asset information.
		/// </summary>
		public AssetGeneral general;
		/// <summary>
		/// Report specific identifier of the event data.
		/// </summary>
		public uint instance;
		/// <summary>
		/// Identifiers of the summary instances that used this event.
		/// </summary>
		public uint[] summaryInstances;
		/// <summary>
		/// The Task used.
		/// </summary>
		public DispatchTask task;
	}

	/// <summary> 
	/// The options used by the report runner to process results.
	/// </summary>
	public partial class ReportOptions {
		/// <summary>
		/// The mechanism to use for filtering based on places and regions.
		/// </summary>
		public ReportFilterMode filtering;
		/// <summary>
		/// A list of parameters to better shape the results.
		/// </summary>
		public ReportParameter[] parameters;
		/// <summary>
		/// A targeting expression for limiting results which only include data from Assets interacting with the targeted Places.
		/// </summary>
		public expression places;
		/// <summary>
		/// A list of provinces and states, where only assets within those regions will be included in the results.
		/// </summary>
		public string[] regions;
		/// <summary>
		/// A targeting expression for including/excluding Assets.
		/// </summary>
		public expression targets;
	}

	/// <summary> 
	/// An argument passed to the report runner.
	/// </summary>
	public partial class ReportParameter {
		/// <summary>
		/// The type of argument.
		/// </summary>
		public ReportParameterType type;
		/// <summary>
		/// The parsed value of the argument. Each type of argument has a different parsing.
		/// </summary>
		public string value;
	}

	/// <summary> 
	/// Determines when and how often a report schedule runs automatically.
	/// </summary>
	public partial class ReportRecurrence {
		/// <summary>
		/// The optional time when the schedule stops recurring in local-time (not UTC).
		/// </summary>
		public DateTime? end;
		/// <summary>
		/// The number of times this schedule has been invoked to generate results.
		/// </summary>
		public ushort iterations;
		/// <summary>
		/// How often the report is automatically run. Daily, weekly, monthly, etc...
		/// </summary>
		public ReportRecurrenceType kind;
		/// <summary>
		/// The date/time stamp from the last result used to inform the nextStartDate and nextEndDate properties. This value is null when the schedule has not yet run once.
		/// </summary>
		public DateTime? lastEndDate;
		/// <summary>
		/// The unique identifier of the last ReportResult generated by this schedule.
		/// </summary>
		public ulong? lastResult;
		/// <summary>
		/// The date/time stamp from the last result used to inform the nextStartDate and nextEndDate properties. This value is null when the schedule has not yet run once.
		/// </summary>
		public DateTime? lastStartDate;
		/// <summary>
		/// This date/time is used as the endDate ReportParameter for the next iteration of this recurring report. This value is null when the schedule is calculated to stop recurring.
		/// </summary>
		public DateTime? nextEndDate;
		/// <summary>
		/// This date/time is used as the startDate ReportParameter for the next iteration of this recurring report. This value is null when the schedule is calculated to stop recurring.
		/// </summary>
		public DateTime? nextStartDate;
		/// <summary>
		/// When the schedule is to begin recurring in local-time (not UTC).
		/// </summary>
		public DateTime start;
		/// <summary>
		/// The local timezone used to calculate recurring date/time ranges.
		/// </summary>
		public string timezone;
		/// <summary>
		/// Used only for weekly schedules, it's a number between 0 and 6 representing the day of the week, with Sunday being the first day of the week.
		/// </summary>
		public byte weekday;
		/// <summary>
		/// Used only for daily schedules, this 7 item, boolean array, determines if the schedule should recur on that day of the week.
		/// </summary>
		public bool[] weekdays;
	}

	/// <summary> 
	/// Report results
	/// </summary>
	public partial class ReportResult {
		/// <summary>
		/// Preserve these results for later review. Results are regularly culled from the system.
		/// </summary>
		public bool archive;
		/// <summary>
		/// After processing, the boundary of the results are given so that a map can be focused on that area.
		/// </summary>
		public LatLngBounds bounds;
		/// <summary>
		/// The company to which this report belongs
		/// </summary>
		public ulong company;
		/// <summary>
		/// The date/time this result was finished processing.
		/// </summary>
		public DateTime? completed;
		/// <summary>
		/// The date/time this result was requested.
		/// </summary>
		public DateTime created;
		/// <summary>
		/// A field which contains report error details if the status is failed.
		/// </summary>
		/// <remarks>max-length: 250</remarks>
		public string error;
		/// <summary>
		/// When the report runs, a list of filtered places is calculated based on the ReportOption's place filtering expression.
		/// </summary>
		public ulong[] filtered;
		/// <summary>
		/// Unique identifier
		/// </summary>
		public ulong id;
		/// <summary>
		/// Name of this report.
		/// </summary>
		/// <remarks>max-length: 100</remarks>
		public string name;
		/// <summary>
		/// Notes about this report.
		/// </summary>
		public string notes;
		/// <summary>
		/// Specified parameters for the report logic, targeted Assets, and filtering Places.
		/// </summary>
		public ReportOptions options;
		/// <summary>
		/// The progress in processing/saving this result is a number between 0 and 100.
		/// </summary>
		public byte progress;
		/// <summary>
		/// The login of the user that ran this report.
		/// </summary>
		/// <remarks>max-length: 254</remarks>
		public email runBy;
		/// <summary>
		/// The processing status of this report.
		/// </summary>
		public ReportStatus status;
		/// <summary>
		/// When the report runs, a list of targeted assets is calculated based on the ReportOption's targeting expression.
		/// </summary>
		public ulong[] targeted;
		/// <summary>
		/// A reference to the Template used to create this result. This field is optional because templates are not necessarily required; they just make life a lot easier.
		/// </summary>
		public ulong? template;
		/// <summary>
		/// The timezone code used to adjust dates/times used in processing and saving this report.
		/// </summary>
		public string timezone;
		/// <summary>
		/// After processing, the report totals the values from all summary instances for a quick overview of the kind of results generated.
		/// </summary>
		public ReportTotal[] totals;
		/// <summary>
		/// Refers to the type of logic used by this report.
		/// </summary>
		public ReportType type;
		/// <summary>
		/// Object version keys used to validate synchronization for all object properties.
		/// </summary>
		public uint[] v;
	}

	/// <summary> 
	/// Relevant details of report results which was deleted.
	/// </summary>
	public partial class ReportResultDeleted {
		/// <summary>
		/// The company to which this report results belonged.
		/// </summary>
		public ulong company;
		/// <summary>
		/// Unique identifier of this report template.
		/// </summary>
		public ulong id;
		/// <summary>
		/// The report template to which these report results belonged.
		/// </summary>
		public ulong template;
	}
	/// <summary> 
	/// Summarized asset details.
	/// </summary>
	public partial class ReportSummary {
		/// <summary>
		/// The asset to which this summary instance belongs.
		/// </summary>
		public ulong asset;
		/// <summary>
		/// The distance travelled in kilometres by the asset during this summary instance.
		/// </summary>
		public double distance;
		/// <summary>
		/// The reason code that this summary instance ended.
		/// </summary>
		public ReportDataSummaryReason endingReason;
		/// <summary>
		/// Date/time stamp of the last event in this summary's sequence.
		/// </summary>
		public DateTime endingUtc;
		/// <summary>
		/// The first asset state which begins this summary instance.
		/// </summary>
		public Asset firstState;
		/// <summary>
		/// 
		/// </summary>
		public string FULL_ADVANCED;
		/// <summary>
		/// 
		/// </summary>
		public string FULL_GENERAL;
		/// <summary>
		/// 
		/// </summary>
		public string FULL_MEGGASE;
		/// <summary>
		/// 
		/// </summary>
		public string FULL_TASK;
		/// <summary>
		/// Identifier of the summary instance in the report.
		/// </summary>
		public uint instance;
		/// <summary>
		/// The number of events included in calculating this summary instance.
		/// </summary>
		public uint instancesCount;
		/// <summary>
		/// The asset state that ended this summary instance.
		/// </summary>
		public Asset lastState;
		/// <summary>
		/// A simplified polyline of all the asset's positions in sequence.
		/// </summary>
		public LatLng[] polyline;
		/// <summary>
		/// The reason code that this summary instance began.
		/// </summary>
		public ReportDataSummaryReason startingReason;
		/// <summary>
		/// Date/time stamp of the first event in this summary's sequence.
		/// </summary>
		public DateTime startingUtc;
		/// <summary>
		/// Code given to this summary instance for an asset.
		/// </summary>
		/// <remarks>max-length: 100</remarks>
		public string stateDetail;
	}

	/// <summary> 
	/// A partially created report used to quickly buil.d results.
	/// </summary>
	public partial class ReportTemplate {
		/// <summary>
		/// The company to which this report belongs
		/// </summary>
		public ulong company;
		/// <summary>
		/// The fill/background colour of the icon. Should be a hex colour in the format #RRGGBB.
		/// </summary>
		/// <remarks>max-length: 22</remarks>
		public string fill;
		/// <summary>
		/// The name of the symbol for this report.
		/// </summary>
		/// <remarks>max-length: 22</remarks>
		public codified graphic;
		/// <summary>
		/// Unique identifier
		/// </summary>
		public ulong id;
		/// <summary>
		/// Name of this report.
		/// </summary>
		/// <remarks>max-length: 100</remarks>
		public string name;
		/// <summary>
		/// Notes about this report.
		/// </summary>
		public string notes;
		/// <summary>
		/// A targeting expression to identify which assets receive the report results. The results emailed to each asset will only be for themselves, not all assets. To receive the emailed results, the Asset must have a messagingAddress , or for a Person type asset, their Contact.email .
		/// </summary>
		/// <remarks>max-length: 255</remarks>
		public expression notifyAssets;
		/// <summary>
		/// List of users to send emailed report. Each email will only contain the results for the assets each user is allowed to view.
		/// </summary>
		/// <remarks>max-length: 50</remarks>
		public string[] notifyUsers;
		/// <summary>
		/// Specified parameters for the report logic, targeted Assets, and filtering Places.
		/// </summary>
		public ReportOptions options;
		/// <summary>
		/// The optional recurring schedule for this report.
		/// </summary>
		public ReportRecurrence schedule;
		/// <summary>
		/// Outline and graphic colour. Should be a hex colour in the format #RRGGBB.
		/// </summary>
		/// <remarks>max-length: 22</remarks>
		public string stroke;
		/// <summary>
		/// Refers to the type of logic used by this report.
		/// </summary>
		public ReportType type;
		/// <summary>
		/// Object version keys used to validate synchronization for all object properties.
		/// </summary>
		public uint[] v;
	}

	/// <summary> 
	/// Relevant details of a report template which was deleted.
	/// </summary>
	public partial class ReportTemplateDeleted {
		/// <summary>
		/// The company to which this report template belonged.
		/// </summary>
		public ulong company;
		/// <summary>
		/// Unique identifier of this report template.
		/// </summary>
		public ulong id;
	}

	/// <summary> 
	/// Totalled information from all the results of the report.
	/// </summary>
	public partial class ReportTotal {
		/// <summary>
		/// The asset to which this report total belongs.
		/// </summary>
		public ulong asset;
		/// <summary>
		/// The total distance travelled in kilometres of all summary instances.
		/// </summary>
		public double distance;
		/// <summary>
		/// The total duration of all summary instances.
		/// </summary>
		public TimeSpan duration;
		/// <summary>
		/// Unique code given to the report total.
		/// </summary>
		/// <remarks>max-length: 100</remarks>
		public string stateDetail;
		/// <summary>
		/// The number of summary instances included in this total.
		/// </summary>
		public uint summaryCount;
		/// <summary>
		/// The total value of all summary instances.
		/// </summary>
		public double value;
		/// <summary>
		/// The type of totalled value.
		/// </summary>
		public string valueType;
	}

	#endregion Reports
	#region Self
	/// <summary> 
	/// Details about your session and yourself.
	/// </summary>
	public partial class Session {
		/// <summary>
		/// The company to which this user belongs.
		/// </summary>
		public ulong company;
		/// <summary>
		/// Your user's simplified contact card.
		///  **Warning:** This object will be serialized as Contact instead of ContactSimple starting in January 2018.
		/// </summary>
		public Contact contact;
		/// <summary>
		/// Indicates whether system access is disable.
		/// </summary>
		public bool enabled;
		/// <summary>
		/// The format strings defining the preferred way to display ambiguous values.
		/// The object's keys follow the codified format.
		/// </summary>
		/// <remarks>max-values-length: 20</remarks>
		/// <seealso cref="codified" />
		public Dictionary<codified, string> formats;
		/// <summary>
		/// List of groups of which you are a member.
		/// </summary>
		public UserGroup[] groups;
		/// <summary>
		/// Preferred region/language for the UI and notifications. Valid formats use &lt;ISO 639-1&gt;&lt;dash&gt;&lt;ISO 3166-2&gt; such as "fr-CA" or "en-US".
		/// </summary>
		/// <remarks>min-length: 2</remarks>
		/// <remarks>max-length: 5</remarks>
		public string language;
		/// <summary>
		/// The unique public email address used to access the system.
		/// </summary>
		/// <remarks>min-length: 6</remarks>
		/// <remarks>max-length: 254</remarks>
		public email login;
		/// <summary>
		/// Preferred way of displaying ambiguous numbers in the context of measurements.
		/// The object's keys follow the codified format.
		/// </summary>
		/// <seealso cref="codified" />
		public Dictionary<codified, SystemsOfUnits> measurements;
		/// <summary>
		/// Human friendly name for these credentials
		/// </summary>
		/// <remarks>max-length: 100</remarks>
		public string nickname;
		/// <summary>
		/// Definition of how and when to send alerts to the user.
		/// </summary>
		/// <remarks>max-length: 7</remarks>
		public UserNotifications[] notify;
		/// <summary>
		/// Additional options which do not fit in with the formats or measurements preferences.
		/// The object's keys follow the codified format.
		/// </summary>
		/// <remarks>max-values-length: 20</remarks>
		/// <seealso cref="codified" />
		public Dictionary<codified, string> options;
		/// <summary>
		/// Indicated whether the credentials have expired according to the company's policy.
		/// </summary>
		public bool passwordExpired;
		/// <summary>
		/// Your company's password complexity and expiry policy.
		/// </summary>
		public PasswordPolicy passwordPolicy;
		/// <summary>
		/// Individual permission rules which override the group rules.
		/// </summary>
		public UserPermission[] permissions;
		/// <summary>
		/// Your company's session lifetime policy.
		/// </summary>
		public SessionPolicy sessionPolicy;
		/// <summary>
		/// The user's local timezone.
		/// </summary>
		public string timezone;
		/// <summary>
		/// Object version keys used to validate synchronization for all object properties.
		/// </summary>
		public uint[] v;
	}
	#endregion Self
	#region Sessions
	/// <summary> 
	/// A structure to provide details about other users' sessions.
	/// </summary>
	public partial class SessionHandle {
		/// <summary>
		/// Indicates that there is at least one active WebSocket connection.
		/// </summary>
		public bool active;
		/// <summary>
		/// Unique identifier of the company to which this session's user belongs.
		/// </summary>
		public ulong company;
		/// <summary>
		/// Date/time stamp from the moment this session was created.
		/// </summary>
		public DateTime created;
		/// <summary>
		/// The unique handle (not identifier) used to manipulate sessions.
		/// </summary>
		public string handle;
		/// <summary>
		/// Last reported IP address used by the client.
		/// </summary>
		public ipv4 ipAddress;
		/// <summary>
		/// Date/time stamp of the last command sent to the system.
		/// </summary>
		public DateTime lastActivity;
		/// <summary>
		/// This session's username.
		/// </summary>
		public email login;
		/// <summary>
		/// The number of WebSocket connections in use with this session.
		/// </summary>
		public uint sockets;
		/// <summary>
		/// Client software identification.
		/// </summary>
		public string userAgent;
	}
	#endregion Sessions
	#region Socket
	/// <summary>
	/// The kinds of broadcast messages sent to all connected clients.
	/// </summary>
	public enum BroadcastType {
		/// <summary> 
		/// Notification of impending maintenance window involving downtime. During a maintenance window, the service may go down and come back online repeatedly until the window ends.
		/// </summary>
		maintenance,
		/// <summary> 
		/// Notification that an upgrade will be available.
		/// </summary>
		upgrade
	}

	/// <summary>
	/// The types of subscriptions available using subscribe.
	/// </summary>
	public enum SubscriptionType {
		/// <summary> 
		/// Rule definitions for the calculation of real-time analytics.
		/// </summary>
		analyticRule,
		/// <summary> 
		/// Asset level updates to the real-time calculations of analytics rules.
		/// </summary>
		analyticSummary,
		/// <summary> 
		/// Assets' advanced properties such as position, attributes, and status tags.
		/// </summary>
		assetAdvanced,
		/// <summary> 
		/// Assets' general properties such as name, icon, and labels.
		/// </summary>
		assetGeneral,
		/// <summary> 
		/// Messages from assets and Alerts to the session user.
		/// </summary>
		assetMessage,
		/// <summary> 
		/// Configured behaviours.
		/// </summary>
		behaviour,
		/// <summary> 
		/// Behaviour log messages to help developers debug their scripts.
		/// </summary>
		behaviourLog,
		/// <summary> 
		/// Behaviour script logic.
		/// </summary>
		behaviourScript,
		/// <summary> 
		/// Renaming and changing the nodes of a company.
		/// </summary>
		companyGeneral,
		/// <summary> 
		/// Company's label and tag styles.
		/// </summary>
		companyLabels,
		/// <summary> 
		/// Company's SessionPolicy and PasswordPolicy.
		/// </summary>
		companyPolicies,
		/// <summary> 
		/// Contact information.
		/// </summary>
		contact,
		/// <summary> 
		/// Assets' tasks information.
		/// </summary>
		dispatchTask,
		/// <summary> 
		/// Hours of Service carriers.
		/// </summary>
		hosCarrier,
		/// <summary> 
		/// Driver's E-log event records.
		/// </summary>
		hosEvent,
		/// <summary> 
		/// Driver vehicle inspection reports.
		/// </summary>
		hosInspection,
		/// <summary> 
		/// Synchronizes icon information. This does not automatically send the picture, that must be requested through the Pictures API.
		/// </summary>
		icon,
		/// <summary> 
		/// Historical vehicle and trailer maintenance work.
		/// </summary>
		maintenanceJob,
		/// <summary> 
		/// Recurring maintenance work for vehicles and trailers.
		/// </summary>
		maintenanceSchedule,
		/// <summary> 
		/// Synchronizes picture information. This does not automatically send the picture, that must be requested through the Pictures API.
		/// </summary>
		picture,
		/// <summary> 
		/// Synchronizes place information
		/// </summary>
		placeGeneral,
		/// <summary> 
		/// Raw provider (device) data like GPS coordinates and parsed ODB-II values.
		/// </summary>
		providerAdvanced,
		/// <summary> 
		/// Provider (device) configurations.
		/// </summary>
		providerConfiguration,
		/// <summary> 
		/// Provider (device) information like name, notes, and selected asset.
		/// </summary>
		providerGeneral,
		/// <summary> 
		/// Historical asset details like breadcrumb trails. The full report results are not sent, only the totals. You need to use the Reports API to request the summary and breakdown.
		/// </summary>
		reportResult,
		/// <summary> 
		/// Report configurations
		/// </summary>
		reportTemplate,
		/// <summary> 
		/// User information such as permissions and group membership.
		/// </summary>
		userAdvanced,
		/// <summary> 
		/// General user information such as name, contact information, and preferences.
		/// </summary>
		userGeneral,
		/// <summary> 
		/// Group information for easy access control.
		/// </summary>
		userGroup
	}

	/// <summary> 
	/// Notification to clients of a scheduled maintenance window. During a maintenance window, the service may go down and come back online repeatedly until the window ends.
	/// </summary>
	public partial class BroadcastMaintenance {
		/// <summary>
		/// Timestamp of when the maintenance will end.
		/// </summary>
		public DateTime ending;
		/// <summary>
		/// The type of broadcast message.
		/// </summary>
		public BroadcastType kind;
		/// <summary>
		/// Human readable message describing the broadcast.
		/// </summary>
		public string message;
		/// <summary>
		/// The UTC date/time of the server hosting the connection.
		/// </summary>
		public DateTime serverTime;
		/// <summary>
		/// Timestamp of when the maintenance window begins.
		/// </summary>
		public DateTime starting;
	}

	/// <summary> 
	/// Notification to clients that an upgrade will take place (with optional requirement of reloading all resources).
	/// </summary>
	public partial class BroadcastUpgrade {
		/// <summary>
		/// Timestamp of when the upgrade will be ready.
		/// </summary>
		public DateTime eta;
		/// <summary>
		/// The type of broadcast message.
		/// </summary>
		public BroadcastType kind;
		/// <summary>
		/// Human readable message describing the broadcast.
		/// </summary>
		public string message;
		/// <summary>
		/// True when system objects have changed structure. It is recommended that all resources are purged and reloaded.
		/// </summary>
		public bool reload;
		/// <summary>
		/// The UTC date/time of the server hosting the connection.
		/// </summary>
		public DateTime serverTime;
	}

	/// <summary> 
	/// A message sent by the WebSocket contains session and expiry information. Sent as first message after connection, and as response to login or getSessionDetails command.
	/// </summary>
	public partial class SessionResponse {
		/// <summary>
		/// The unique, numeric error code when processing this request.
		/// </summary>
		public ErrorCode errorCode;
		/// <summary>
		/// An object to provide developers with a hint about the nature of the error. The key is not always present, and only available for some errors.
		/// </summary>
		public object errorDetails;
		/// <summary>
		/// The date/time when this Session will expire.
		/// </summary>
		public DateTime expiry;
		/// <summary>
		/// The UserSession's id.
		/// </summary>
		public string ghostId;
		/// <summary>
		/// An English description of the error.
		/// </summary>
		public string message;
		/// <summary>
		/// The company's password policy. Needed if the user's password is expired.
		/// </summary>
		public PasswordPolicy passwordPolicy;
		/// <summary>
		/// The optional, client-generated unique identifier for correlating requests. The key is only present if a reqId was given for the associated request.
		/// </summary>
		public uint? reqId;
		/// <summary>
		/// The UTC date/time of the server hosting the connection.
		/// </summary>
		public DateTime serverTime;
		/// <summary>
		/// The company's session policy.
		/// </summary>
		public SessionPolicy sessionPolicy;
		/// <summary>
		/// The session's user object.
		/// </summary>
		public Session user;
	}

	/// <summary> 
	/// The WebSocket will respond to each request with the reqId it was given so you can correlate responses.
	/// </summary>
	public partial class SocketRequest {
		/// <summary>
		/// An optional, client-generated unique identifier for correlating responses.
		/// </summary>
		public uint reqId;
	}

	/// <summary> 
	/// It will always have the reqId, errorCode, and message properties, but can also contain any number of other properties.
	/// </summary>
	public partial class SocketResponse {
		/// <summary>
		/// The unique, numeric error code when processing this request.
		/// </summary>
		public ErrorCode errorCode;
		/// <summary>
		/// An object to provide developers with a hint about the nature of the error. The key is not always present, and only available for some errors.
		/// </summary>
		public object errorDetails;
		/// <summary>
		/// An English description of the error.
		/// </summary>
		public string message;
		/// <summary>
		/// The optional, client-generated unique identifier for correlating requests. The key is only present if a reqId was given for the associated request.
		/// </summary>
		public uint? reqId;
	}

	/// <summary> 
	/// A per-company list of your currect SubscriptionTypes.
	/// </summary>
	public partial class Subscription {
		/// <summary>
		/// The company relevant to the notification types you want to receive.
		/// </summary>
		public ulong company;
		/// <summary>
		/// List of notification types for the company.
		/// </summary>
		public SubscriptionType[] subscriptionTypes;
	}
	#endregion Socket
	#region Users
	/// <summary>
	/// The types of alerts used.
	/// </summary>
	public enum NotificationsMethod {
		/// <summary> 
		/// Carrier pigeon.
		/// </summary>
		email,
		/// <summary> 
		/// A separate message sent across the WebSocket.
		/// </summary>
		popup,
		/// <summary> 
		/// A text message (SMS).
		/// </summary>
		sms
	}

	/// <summary>
	/// The levels of permission available.
	/// </summary>
	public enum PermissionLevel {
		/// <summary> 
		/// Full control to read, write, delete and create things.
		/// </summary>
		full,
		/// <summary> 
		/// Read-only access, no changes allowed.
		/// </summary>
		read,
		/// <summary> 
		/// Read and write access, but things cannot be deleted or new things created.
		/// </summary>
		update
	}

	/// <summary>
	/// How a permission is applied.
	/// </summary>
	public enum PermissionMethod {
		/// <summary> 
		/// Permission is given.
		/// </summary>
		grant,
		/// <summary> 
		/// Permission is taken away.
		/// </summary>
		revoke
	}

	/// <summary>
	/// The kinds of permissions available for a UserPermission.
	/// </summary>
	public enum PermissionType {
		/// <summary> 
		/// Rules definiting real-time analytic calculations.
		/// </summary>
		analyticRule,
		/// <summary> 
		/// Updates to the calculations of real-time analytics.
		/// </summary>
		analyticSummary,
		/// <summary> 
		/// Assets' advanced properties such as position, attributes, and status tags.
		/// </summary>
		assetAdvanced,
		/// <summary> 
		/// Assets' general properties such as name, icon, and labels.
		/// </summary>
		assetGeneral,
		/// <summary> 
		/// Messages from assets and Alerts to the session user.
		/// </summary>
		assetMessage,
		/// <summary> 
		/// Configured behaviours.
		/// </summary>
		behaviour,
		/// <summary> 
		/// View and clear the log of debug messages for a behaviour or script.
		/// </summary>
		behaviourLog,
		/// <summary> 
		/// Behaviour script logic.
		/// </summary>
		behaviourScript,
		/// <summary> 
		/// Adjusting the billing details of a company.
		/// </summary>
		companyBilling,
		/// <summary> 
		/// Access to add a new child company.
		/// </summary>
		companyCreate,
		/// <summary> 
		/// Basic information about the company. This permissions is required to have access to other aspects of the company.
		/// </summary>
		companyGeneral,
		/// <summary> 
		/// Company's label and tag styles.
		/// </summary>
		companyLabels,
		/// <summary> 
		/// Company's SessionPolicy and PasswordPolicy.
		/// </summary>
		companyPolicies,
		/// <summary> 
		/// Contact information.
		/// </summary>
		contact,
		/// <summary> 
		/// Assets' tasks information.
		/// </summary>
		dispatchTask,
		/// <summary> 
		/// Hours of Service Carriers.
		/// </summary>
		hosCarrier,
		/// <summary> 
		/// Driver's E-log event records.
		/// </summary>
		hosEvent,
		/// <summary> 
		/// Driver vehicle inspection reports.
		/// </summary>
		hosInspection,
		/// <summary> 
		/// Icon information.
		/// </summary>
		icon,
		/// <summary> 
		/// Historical vehicle and trailer maintenance work.
		/// </summary>
		maintenanceJob,
		/// <summary> 
		/// Recurring maintenance work for vehicles and trailers.
		/// </summary>
		maintenanceSchedule,
		/// <summary> 
		/// Picture information.
		/// </summary>
		picture,
		/// <summary> 
		/// Place information.
		/// </summary>
		placeGeneral,
		/// <summary> 
		/// Raw provider (device) data like GPS coordinates and parsed ODB-II values.
		/// </summary>
		providerAdvanced,
		/// <summary> 
		/// Allows read-only access to the logic types for provider configuration.
		/// </summary>
		providerConfigType,
		/// <summary> 
		/// Provider (device) configurations.
		/// </summary>
		providerConfiguration,
		/// <summary> 
		/// Provider (device) information like name, notes, and selected asset.
		/// </summary>
		providerGeneral,
		/// <summary> 
		/// Historical asset details like breadcrumb trails.
		/// </summary>
		reportResult,
		/// <summary> 
		/// Report configurations.
		/// </summary>
		reportTemplate,
		/// <summary> 
		/// Access to retrieve a list of active sessions and kill sessions.
		/// </summary>
		session,
		/// <summary> 
		/// User information such as permissions and group membership.
		/// </summary>
		userAdvanced,
		/// <summary> 
		/// General user information such as name, contact information, and preferences.
		/// </summary>
		userGeneral,
		/// <summary> 
		/// Group information for easy access control.
		/// </summary>
		userGroup
	}

	/// <summary>
	/// A type of measurement system
	/// </summary>
	public enum SystemsOfUnits {
		/// <summary> 
		/// British Standard
		/// </summary>
		british,
		/// <summary> 
		/// Metric
		/// </summary>
		metric,
		/// <summary> 
		/// US Standard.
		/// </summary>
		standard
	}

	/// <summary> 
	/// A grouping of credentials, information, preferences, and permissions for a person or machine to login to the system and access its resources.
	/// </summary>
	public partial class User {
		/// <summary>
		/// The company to which this user belongs.
		/// </summary>
		public ulong company;
		/// <summary>
		/// Contact information for this user.
		/// </summary>
		public ulong? contact;
		/// <summary>
		/// Indicates whether system access is disable.
		/// </summary>
		public bool enabled;
		/// <summary>
		/// The format strings defining the preferred way to display ambiguous values.
		/// The object's keys follow the codified format.
		/// </summary>
		/// <remarks>max-values-length: 20</remarks>
		/// <seealso cref="codified" />
		public Dictionary<codified, string> formats;
		/// <summary>
		/// A list of groups to which this user belongs.
		/// </summary>
		public ulong[] groups;
		/// <summary>
		/// Preferred region/language for the UI and notifications. Valid formats use &lt;ISO 639-1&gt;&lt;dash&gt;&lt;ISO 3166-2&gt; such as "fr-CA" or "en-US".
		/// </summary>
		/// <remarks>min-length: 2</remarks>
		/// <remarks>max-length: 5</remarks>
		public string language;
		/// <summary>
		/// The unique public email address used to access the system.
		/// </summary>
		/// <remarks>min-length: 6</remarks>
		/// <remarks>max-length: 254</remarks>
		public email login;
		/// <summary>
		/// Preferred way of displaying ambiguous numbers in the context of measurements.
		/// The object's keys follow the codified format.
		/// </summary>
		/// <seealso cref="codified" />
		public Dictionary<codified, SystemsOfUnits> measurements;
		/// <summary>
		/// Human friendly name for these credentials
		/// </summary>
		/// <remarks>max-length: 100</remarks>
		public string nickname;
		/// <summary>
		/// Definition of how and when to send alerts to the user.
		/// </summary>
		/// <remarks>max-length: 7</remarks>
		public UserNotifications[] notify;
		/// <summary>
		/// Additional options which do not fit in with the formats or measurements preferences.
		/// The object's keys follow the codified format.
		/// </summary>
		/// <remarks>max-values-length: 20</remarks>
		/// <seealso cref="codified" />
		public Dictionary<codified, string> options;
		/// <summary>
		/// Indicated whether the credentials have expired according to the company's policy.
		/// </summary>
		public bool passwordExpired;
		/// <summary>
		/// Individual permission rules which override the group rules.
		/// </summary>
		public UserPermission[] permissions;
		/// <summary>
		/// The user's local timezone.
		/// </summary>
		public string timezone;
		/// <summary>
		/// Object version keys used to validate synchronization for different properties.
		/// v[0]: Properties found in the UserGeneral object.
		/// v[1]: Properties found in the UserAdvanced object.
		/// </summary>
		/// <remarks>length: 2</remarks>
		public uint[] v;
	}

	/// <summary> 
	/// Permissions and group membership defined for a user.
	/// </summary>
	public partial class UserAdvanced {
		/// <summary>
		/// The company to which this user belongs.
		/// </summary>
		public ulong company;
		/// <summary>
		/// A list of groups to which this user belongs.
		/// </summary>
		public ulong[] groups;
		/// <summary>
		/// The unique public email address used to access the system.
		/// </summary>
		/// <remarks>min-length: 6</remarks>
		/// <remarks>max-length: 254</remarks>
		public email login;
		/// <summary>
		/// Individual permission rules which override the group rules.
		/// </summary>
		public UserPermission[] permissions;
		/// <summary>
		/// Object version keys used to validate synchronization for all object properties.
		/// </summary>
		public uint[] v;
	}

	/// <summary> 
	/// Relevant details of a user which was deleted.
	/// </summary>
	public partial class UserDeleted {
		/// <summary>
		/// The company to which this user belonged.
		/// </summary>
		public ulong company;
		/// <summary>
		/// Unique identifier of the user.
		/// </summary>
		public email login;
	}
	/// <summary> 
	/// Credentials, information, and preferences about a user.
	/// </summary>
	public partial class UserGeneral {
		/// <summary>
		/// The company to which this user belongs.
		/// </summary>
		public ulong company;
		/// <summary>
		/// Contact information for this user.
		/// </summary>
		public ulong? contact;
		/// <summary>
		/// Indicates whether system access is disable.
		/// </summary>
		public bool enabled;
		/// <summary>
		/// The format strings defining the preferred way to display ambiguous values.
		/// The object's keys follow the codified format.
		/// </summary>
		/// <remarks>max-values-length: 20</remarks>
		/// <seealso cref="codified" />
		public Dictionary<codified, string> formats;
		/// <summary>
		/// Preferred region/language for the UI and notifications. Valid formats use &lt;ISO 639-1&gt;&lt;dash&gt;&lt;ISO 3166-2&gt; such as "fr-CA" or "en-US".
		/// </summary>
		/// <remarks>min-length: 2</remarks>
		/// <remarks>max-length: 5</remarks>
		public string language;
		/// <summary>
		/// The unique public email address used to access the system.
		/// </summary>
		/// <remarks>min-length: 6</remarks>
		/// <remarks>max-length: 254</remarks>
		public email login;
		/// <summary>
		/// Preferred way of displaying ambiguous numbers in the context of measurements.
		/// The object's keys follow the codified format.
		/// </summary>
		/// <seealso cref="codified" />
		public Dictionary<codified, SystemsOfUnits> measurements;
		/// <summary>
		/// Human friendly name for these credentials
		/// </summary>
		/// <remarks>max-length: 100</remarks>
		public string nickname;
		/// <summary>
		/// Definition of how and when to send alerts to the user.
		/// </summary>
		/// <remarks>max-length: 7</remarks>
		public UserNotifications[] notify;
		/// <summary>
		/// Additional options which do not fit in with the formats or measurements preferences.
		/// The object's keys follow the codified format.
		/// </summary>
		/// <remarks>max-values-length: 20</remarks>
		/// <seealso cref="codified" />
		public Dictionary<codified, string> options;
		/// <summary>
		/// Indicated whether the credentials have expired according to the company's policy.
		/// </summary>
		public bool passwordExpired;
		/// <summary>
		/// The user's local timezone.
		/// </summary>
		public string timezone;
		/// <summary>
		/// Object version keys used to validate synchronization for all object properties.
		/// </summary>
		public uint[] v;
	}

	/// <summary> 
	/// A group of permissions that allow for easier user permission control.
	/// </summary>
	public partial class UserGroup {
		/// <summary>
		/// The company to which this group belongs.
		/// </summary>
		public ulong company;
		/// <summary>
		/// Unique identifier of this group.
		/// </summary>
		public ulong id;
		/// <summary>
		/// A name given to this group.
		/// </summary>
		/// <remarks>max-length: 100</remarks>
		public string name;
		/// <summary>
		/// Notes about this group, and to whom this group should be applied.
		/// </summary>
		public string notes;
		/// <summary>
		/// Permissions for this group.
		/// </summary>
		public UserPermission[] permissions;
		/// <summary>
		/// Object version keys used to validate synchronization for all object properties.
		/// </summary>
		public uint[] v;
	}

	/// <summary> 
	/// Relevant details of a group which was deleted.
	/// </summary>
	public partial class UserGroupDeleted {
		/// <summary>
		/// The company to which this group belonged.
		/// </summary>
		public ulong company;
		/// <summary>
		/// Unique identifier of this group.
		/// </summary>
		public ulong id;
	}

	/// <summary> 
	/// Definition of how and when to send alerts to the user.
	/// </summary>
	public partial class UserNotifications {
		/// <summary>
		/// Email address where the sent is sent. If not specified, the email address from the User's Contact is taken. If the contact has no email address, the alert is sent to the user's login.
		/// </summary>
		/// <remarks>min-length: 6</remarks>
		/// <remarks>max-length: 254</remarks>
		public email email;
		/// <summary>
		/// A flag for whether or not this schedule is in use.
		/// </summary>
		public bool enabled;
		/// <summary>
		/// End time portion of the schedule that defines a period of the day when the user wants to receive alerts. The time value is defined in local time, not UTC.
		/// </summary>
		public TimeSpan end;
		/// <summary>
		/// A common name like "Weekdays" or "Off Hours".
		/// </summary>
		/// <remarks>max-length: 100</remarks>
		public string name;
		/// <summary>
		/// A list of the types of methods to use to notify the user when they are not connected.
		/// </summary>
		public NotificationsMethod[] offline;
		/// <summary>
		/// A list of the types of methods to use to notify the user when they have an active WebSocket connection.
		/// </summary>
		public NotificationsMethod[] online;
		/// <summary>
		/// SMS address where the alert is sent. If not specified, the mobile phone number from the User's Contact is taken. If the contact has no mobile phone number, the alert is not sent.
		/// </summary>
		public ulong? sms;
		/// <summary>
		/// Start time portion of the schedule that defines a period of the day when the user wants to receive alerts. The time value is defined in local time, not UTC.
		/// </summary>
		public TimeSpan start;
		/// <summary>
		/// A 7 item, boolean array, determines if the user should be notified on that day of the week. The days of the week are defined in local time, not UTC.
		/// </summary>
		public bool[] weekdays;
	}

	/// <summary> 
	/// A defined permission for users and groups.
	/// </summary>
	public partial class UserPermission {
		/// <summary>
		/// The company that this permission targets.
		/// </summary>
		public ulong company;
		/// <summary>
		/// Codified names of labels. If list is empty, this permission applies for all labels.
		/// </summary>
		public string[] labels;
		/// <summary>
		/// The level of access being defined.
		/// </summary>
		public PermissionLevel level;
		/// <summary>
		/// The way the access is used.
		/// </summary>
		public PermissionMethod method;
		/// <summary>
		/// The kind of permission or subscription.
		/// </summary>
		public PermissionType type;
	}

	#endregion Users
}