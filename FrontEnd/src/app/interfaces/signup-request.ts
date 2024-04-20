export interface SignupRequest
{
    username: string
    password: string
    role: 'Admin' | 'User' | 'Guest'
}
