import Environment from './Environment';

export default class Feature {
    id: number;
    name: string;
    environments: Array<Environment>;
}