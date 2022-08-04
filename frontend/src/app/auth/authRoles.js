export const authRoles = {
  Admin: ['Admin'], // Only Admin has access
  Driver: ['Admin', 'Driver'], // Only Admin & Driver has access
  Guest: ['Admin', 'Driver', 'Guest'], // Everyone has access
}
