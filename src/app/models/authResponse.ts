export interface authResponse
{
  userID?: string,
  message?: string,
  isAuthenticated: boolean,
  userName?: string,
  email?: string,
  roles?: string[],
  token?: string,
  refreshTokenExpiretion?: Date,
  tokenExpiretion?: Date
}
