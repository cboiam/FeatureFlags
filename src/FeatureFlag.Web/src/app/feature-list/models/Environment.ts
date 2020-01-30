import User from './User';

export default class Environment {

    constructor() {
        this.usersEnabled = new Array<User>();
    }

    id: number;
    name: string;
    enabled: boolean;
    usersEnabled: Array<User>;
}