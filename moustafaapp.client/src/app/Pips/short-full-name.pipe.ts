import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'shortFullName'
})
export class ShortFullNamePipe implements PipeTransform {

  //transform(value: unknown, ...args: unknown[]): unknown {
  //  return null;
  //}

  transform(fullName: string | null | undefined): string {
    if (!fullName) return '';

    const parts = fullName.trim().split(' ');

    if (parts.length === 1) {
      return parts[0];
    }

    return `${parts[0]} ${parts[1].charAt(0)}.`;
  }

}

