using AutoMapper;
using HIS.AdministrativeGenders;
using HIS.AdministrativeSexes;
using HIS.Authors;
using HIS.ContactModes;
using HIS.Countries;
using HIS.Educations;
using HIS.Facilities;
using HIS.IdentifierTypes;
using HIS.InActiveReasons;
using HIS.Languages;
using HIS.MaritalStatuses;
using HIS.NamePrefixes;
using HIS.Nationalities;
using HIS.Races;
using HIS.Relationships;
using HIS.Religions;

namespace HIS;

public class HISApplicationAutoMapperProfile : Profile
{
    public HISApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<Author, AuthorDto>();
        CreateMap<AuthorDto, Author>();
        CreateMap<AdministrativeSex, AdministrativeSexDto>();
        CreateMap<AdministrativeSexDto, AdministrativeSex>();
        CreateMap<AdministrativeGender, AdministrativeGenderDto>();
        CreateMap<AdministrativeGenderDto, AdministrativeGender>();
        CreateMap<ContactMode, ContactModeDto>();
        CreateMap<ContactModeDto, ContactMode>();
        CreateMap<Country, CountryDto>();
        CreateMap<CountryDto, Country>();
        CreateMap<Education, EducationDto>();
        CreateMap<EducationDto, Education>();
        CreateMap<Facility, FacilityDto>();
        CreateMap<FacilityDto, Facility>();
        CreateMap<IdentifierType, IdentifierTypeDto>();
        CreateMap<IdentifierTypeDto, IdentifierType>();
        CreateMap<InActiveReason, InActiveReasonDto>();
        CreateMap<InActiveReasonDto, InActiveReason>();
        CreateMap<Language, LanguageDto>();
        CreateMap<LanguageDto, Language>();
        CreateMap<MaritalStatus, MaritalStatusDto>();
        CreateMap<MaritalStatusDto, MaritalStatus>();
        CreateMap<Nationality, NationalityDto>();
        CreateMap<NationalityDto, Nationality>();
        CreateMap<Race, RaceDto>();
        CreateMap<RaceDto, Race>();
        CreateMap<Relationship, RelationshipDto>();
        CreateMap<RelationshipDto, Relationship>();
        CreateMap<Religion, ReligionDto>();
        CreateMap<ReligionDto, Religion>();
        CreateMap<NamePrefix, NamePrefixDto>();
        CreateMap<NamePrefixDto, NamePrefix>();
    }
}
