export interface UserRole
{
  userRoles: UserRoleModel[];
}
export interface UserRoleModel {
  roleName: string;
  selected: boolean;
}
