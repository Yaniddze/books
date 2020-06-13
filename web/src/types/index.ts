import { ErrorHttpAction } from '../domain/book/types';

export type FillActionType<T> = (
    payload: T,
) => {
    type: string;
    payload: T;
};

export type ErrorActionType = (
    payload: ErrorHttpAction,
) => {
    type: string;
    payload: ErrorHttpAction;
};
