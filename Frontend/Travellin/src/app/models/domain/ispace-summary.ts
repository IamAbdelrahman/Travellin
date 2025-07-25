import { ISpaceType } from './ispace-type';

export interface ISpaceSummary {
  spaceType: ISpaceType;
  count: number;
  isShared: boolean;
}
