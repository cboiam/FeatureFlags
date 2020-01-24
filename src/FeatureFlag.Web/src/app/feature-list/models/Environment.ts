import User from './User';

export default class Environment {
    id: number;
    name: string;
    enabled: boolean;
    usersEnabled: Array<User>;
}