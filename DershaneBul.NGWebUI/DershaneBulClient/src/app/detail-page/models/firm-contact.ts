export class FirmContact {
  contactDescription: string;
  contactIcon: string;
  contactType: EnumContactType;
} 
export enum EnumContactType
{
  eMail = 1,
  Facebook = 2,
  Phone = 3,
  Twitter = 4,
  Instagram = 5,
  WebSite=6
}
