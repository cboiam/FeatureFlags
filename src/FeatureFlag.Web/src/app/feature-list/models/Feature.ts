import Environment from './Environment';

export default class Feature {

    constructor() {
        this.environments = new Array<Environment>();
    }

    id: number;
    name: string;
    environments: Array<Environment>;
}